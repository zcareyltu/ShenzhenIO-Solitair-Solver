using DotNetPriorityQueue;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public class State {

		private static int idCount = 0;
		private int Id; //For debugging

		private const int trays_count = 8;
		private const int slots_count = 3;


		private float priority;
		private List<Card>[] trays;
		private Card[] slots;
		private int step;
		private State prevState;
		private int remainingCards;
		private Action action;
		private Dictionary<Suit, int> lowestPerSuit;
		private Dictionary<Suit, int> suitSlotIndex;

		public State(State prevState = null, Action action = null, List<Card>[] customTrays = null) {
			this.Id = idCount++;
			if (prevState == null) {
				if (customTrays == null) {
					customTrays = new List<Card>[trays_count];
					for (int i = 0; i < trays_count; i++)
						customTrays[i] = new List<Card>();
				}
				suitSlotIndex = new Dictionary<Suit, int>();

				this.trays = customTrays;
				this.slots = new Card[3];
				this.step = 0;
			} else {
				List<Card>[] prevTrays = prevState.trays;
				suitSlotIndex = prevState.suitSlotIndex;
				this.trays = new List<Card>[trays_count];
				for (int i = 0; i < trays_count; i++) {
					this.trays[i] = new List<Card>(prevTrays[i]);
				}
				this.slots = CreateCopyOfArray(prevState.slots);
				this.step = prevState.step + 1;
			}
			this.prevState = prevState;
			if (action != null) {
				List<Card> cardsToBeMoved = new List<Card>();
				if (action.Collapse != null) {
					Suit target = (Suit)action.Collapse;
					for (int i = 0; i < trays_count; i++) {
						if (this.trays[i].Count == 0)
							continue;
						Card lastCard = this.trays[i].last();
						if (lastCard.IsDragon && lastCard.Suit == target) //Same suit and number, or same suit and dragon???
							this.trays[i].pop();
					}
					for (int i = 0; i < slots_count; i++) {
						if (this.slots[i].IsEmpty)
							continue;
						if (this.slots[i].IsDragon && this.slots[i].Suit == target)
							this.slots[i] = new Card();
					}
					for (int i = 0; i < slots_count; i++) {
						if (this.slots[i].IsEmpty) {
							this.slots[i].Type = CardType.Filled; //this.slots[i] = 'F';
							break;
						}
					}
				} else if (action.Pop != null) {
					this.trays[(int)action.Pop].pop();
				} else {
					if (action.From != null) {
						for (int i = 0; i < action.FromCount; i++)
							cardsToBeMoved.Add(this.trays[(int)action.From].pop());
					} else {
						cardsToBeMoved.Add(this.slots[(int)action.FromSlot]);
						this.slots[(int)action.FromSlot] = new Card();
					}
					if (action.To != null) {
						while (cardsToBeMoved.Count > 0) {
							this.trays[(int)action.To].Add(cardsToBeMoved.pop());
						}
					} else {
						this.slots[(int)action.ToSlot] = cardsToBeMoved[0];
					}
				}
			}

			this.auto_remove_cards();
			this.action = action;
			this.remainingCards = 0;
			for (int i = 0; i < trays_count; i++) {
				this.remainingCards += this.trays[i].Count;
			}
			for (int i = 0; i < slots_count; i++) {
				if (!this.slots[i].IsFlower && !this.slots[i].IsEmpty) {
					this.remainingCards++;
				}
			}
			this.priority = this.calcPriority();
		}

		private void initialSetup() {
			foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
				if (suit == Suit.None) continue;
				if (!suitSlotIndex.ContainsKey(suit)) {
					for (int i = 0; i < 3; i++) {
						if (!suitSlotIndex.ContainsValue(i)) {
							suitSlotIndex[suit] = i;
							break;
						}
					}
				}
			}

			this.auto_remove_cards();
			this.remainingCards = 0;
			for (int i = 0; i < trays_count; i++) {
				this.remainingCards += this.trays[i].Count;
			}
			for (int i = 0; i < slots_count; i++) {
				if (!this.slots[i].IsFlower && !this.slots[i].IsEmpty) {
					this.remainingCards++;
				}
			}
			this.priority = this.calcPriority();
		}

		public void SetCard(int stack, int card, Card newCard) {
			List<Card> tray = trays[stack];
			while(card >= tray.Count) {
				tray.Add(new Card());
			}
			tray[card] = newCard;
		}

		public void SetSuitStack(Suit suit, int index) {
			suitSlotIndex[suit] = index;
		}

		public void CleanTrays() {
			foreach(List<Card> tray in trays) {
				if (tray.last().IsEmpty) {
					tray.pop();
				}
			}
		}

		private static T[] CreateCopyOfArray<T>(T[] orig) {
			T[] copy = new T[orig.Length];
			Array.Copy(orig, copy, orig.Length);
			return copy;
		}

		private void auto_remove_cards() {
			// some logics taken from Nickardson/shenzhen-solitaire
			this.lowestPerSuit = new Dictionary<Suit, int>() {
				{ Suit.Red, 10 },
				{ Suit.Green, 10 },
				{ Suit.Black, 10 }
			}; //this.lowestPerSuit = { 'R': 10, 'G': 10, 'B': 10};
			bool callAgain = false;
			for (int i = 0; i < trays_count; i++) {
				if (this.trays[i].Count == 0)
					continue;
				List<Card> tray = this.trays[i];
				Card last_card = tray.last();
				if (last_card.IsFlower || last_card.Value == 1) { //if flower and card with value 1, remove it
					tray.pop();
					callAgain = true;
				}
				for (int j = 0; j < tray.Count; j++) {
					Card card = tray[j];
					if (card.IsSuit/*card.length == 2 && card[0] != "D"*/)
						if (card.Value < this.lowestPerSuit[card.Suit])
							this.lowestPerSuit[card.Suit] = card.Value;
				}
			}
			for (int i = 0; i < slots_count; i++) {
				if (this.slots[i].IsEmpty)
					continue;
				Card card = this.slots[i];
				if (card.IsSuit && card.Value < this.lowestPerSuit[card.Suit])//card[0] != "D" && card[1] < this.lowestPerSuit[card[0]])
					this.lowestPerSuit[card.Suit] = card.Value;
			}
			for (int i = 0; i < trays_count; i++) {
				if (this.trays[i].Count == 0)
					continue;
				Card card = this.trays[i].last();
				if (card.IsDragon)
					continue;
				int value = card.Value;
				if (value > 2) {
					if (value <= this.lowestPerSuit[Suit.Red] && value <= this.lowestPerSuit[Suit.Green] && value <= this.lowestPerSuit[Suit.Black]) {
						this.trays[i].pop();
						callAgain = true;
					}
				} else if (value == 2 && value == this.lowestPerSuit[card.Suit]) {
					this.trays[i].pop();
					callAgain = true;
				}
			}
			for (int i = 0; i < slots_count; i++) {
				if (this.slots[i].IsEmpty)
					continue;
				Card card = this.slots[i];
				if (card.IsDragon)
					continue;
				int value = card.Value;
				if (value > 2) {
					if (value <= this.lowestPerSuit[Suit.Red] && value <= this.lowestPerSuit[Suit.Green] && value <= this.lowestPerSuit[Suit.Black]) {
						this.slots[i] = new Card();
						callAgain = true;
					}
				} else if (value == 2 && value == this.lowestPerSuit[card.Suit]) {
					this.slots[i] = new Card();
					callAgain = true;
				}
			}
			if (callAgain) {
				Console.WriteLine("panggil lagi");
				this.auto_remove_cards();
			}
		}

		/**
     * Get every valid steps for this state (excluding slots)
     * @return {Array}
     */
		public List<Action> valid_steps() {
			List<Card>[] trays = this.trays;
			List<Action> ret = new List<Action>();
			Dictionary<Suit, int> exposedDragons = new Dictionary<Suit, int> { { Suit.Red, 0 }, { Suit.Green, 0 }, { Suit.Black, 0 } };
			for (var i = 0; i < trays_count; i++) { //for each tray in trays
				var tray = trays[i];
				var j = tray.Count - 1;
				if (tray.Count < 1)
					continue;

				Card card = tray.last();
				if (card.IsDragon) //if it's dragon exposed
					exposedDragons[card.Suit]++;
				if (this.lowestPerSuit[card.Suit] == card.Value) //if the card with lower value already out, remove this card
					ret.Add(Action.GetPop(i, tray.Count - 1, suitSlotIndex[card.Suit])); //ret.push({ pop: i });
				for (j = tray.Count - 1; j >= 0; j--) { //for each card in tray, from bottom to up
					card = tray[j];
					for (int k = 0; k < trays_count; k++) { //for each tray in trays, but not the current tray, get last card
						if (i == k)
							continue;
						if (trays[k].Count > 0) { //if the target tray not empty
							var target = trays[k].last();
							if (can_be_stacked(card, target)) {
								/*ret.push({
									from: { tray: i, count: tray.length - j}, 
									to: { tray: k}
								});*/
								ret.Add(Action.GetFrom(i, j, tray.Count - j, k, trays[k].Count));
							}
						} else if (tray.Count > 1 && j > 0) { //if the target tray empty and not moving entire tray
							/*ret.push({
								from: { tray: i, count: tray.length - j }, 
								to: { tray: k}
							});*/
							ret.Add(Action.GetFrom(i, j, tray.Count - j, k, trays[k].Count));
						}
					}
					if (j > 0 && !can_be_stacked(card, tray[j - 1]))
						break;
				}
			}
			bool slotAvailableForDragon = false;
			Dictionary<Suit, bool> slotAvailableForSpecificDragon = new Dictionary<Suit, bool>() { { Suit.Red, false }, { Suit.Green, false }, { Suit.Black, false } };
			for (var i = 0; i < slots_count; i++) { //check every possible steps from slots
				if (this.slots[i].IsEmpty) {  //if it's available, skip
					slotAvailableForDragon = true;
					continue;
				}
				if (this.slots[i].IsFull) //if it's full, from collapsed dragon, skip
					continue;

				var card = this.slots[i];
				for (var j = 0; j < trays_count; j++) {
					if (trays[j].Count > 0) {
						var target = trays[j].last();
						if (can_be_stacked(this.slots[i], target))
							ret.Add(Action.FromSlotToTray(i, j, trays[j].Count)); //ret.push({ from: { slot: i}, to: { tray: j} });
					} else {
						ret.Add(Action.FromSlotToTray(i, j, trays[j].Count)); //ret.push({ from: { slot: i}, to: { tray: j} });
					}
				}
				if (card.IsDragon) {
					slotAvailableForSpecificDragon[card.Suit] = true;
					exposedDragons[card.Suit]++;
				}
			}

			if (exposedDragons[Suit.Green] == 4 && (slotAvailableForDragon || slotAvailableForSpecificDragon[Suit.Green]))
				ret.Add(Action.GetCollapse(Suit.Green)); //ret.push({ collapse: 'DG'});
			if (exposedDragons[Suit.Red] == 4 && (slotAvailableForDragon || slotAvailableForSpecificDragon[Suit.Red]))
				ret.Add(Action.GetCollapse(Suit.Red)); //ret.push({ collapse: 'DR'});
			if (exposedDragons[Suit.Black] == 4 && (slotAvailableForDragon || slotAvailableForSpecificDragon[Suit.Black]))
				ret.Add(Action.GetCollapse(Suit.Black)); //ret.push({ collapse: 'DW'});

			return ret;
		}

		/**
		 * Get every valid slot steps for this state
		 * @return {Array}
		 */
		private List<Action> valid_slot_steps() {
			List<Action> ret = new List<Action>();
			for (int i = 0; i < trays_count; i++) { //for each tray in trays
				if (this.trays[i].Count == 0) //this tray have no cards, skip!
					continue;
				for (int k = 0; k < slots_count; k++) { //for each slot in slots
					if (this.slots[k].IsEmpty)
						ret.Add(Action.FromTrayToSlot(i, trays[i].Count - 1, 1, k)); //ret.push({ from: { tray: i, count: 1}, to: { slot: k} });
				}
			}
			return ret;
		}

		/**
		 * Calculate prority for this state
		 * @return {Number}
		 */
		private float calcPriority() {
			float stackedCards = 0;
			for (int i = 0; i < trays_count; i++) {
				List<Card> tray = this.trays[i];
				if (tray.Count == 0)
					continue;
				int localStackedCards = 0;
				for (int j = tray.Count - 1; j > 0; j--) {
					if (can_be_stacked(tray[j], tray[j - 1]))
						localStackedCards++;
				}
				if (localStackedCards == tray.Count - 1)
					if (tray.last().Value == 9)
						stackedCards += localStackedCards * 1.2f;
					else stackedCards += localStackedCards * 1.1f;
				else stackedCards += localStackedCards;
			}
			return this.remainingCards + this.step * 0.1f - stackedCards;

			/*int stackedCards = 0;
			for (int i = 0; i < trays_count; i++) {
				List<Card> tray = this.trays[i];
				if (tray.Count == 0)
					continue;
				int localStackedCards = 0;
				for (int j = tray.Count - 1; j > 0; j--) {
					if (can_be_stacked(tray[j], tray[j - 1]))
						localStackedCards++;
				}
				if (localStackedCards == tray.Count - 1)
					if (tray.last().Value == 9)
						stackedCards += localStackedCards * 12;
					else stackedCards += localStackedCards * 11;
				else stackedCards += localStackedCards * 10;
			}*/
			//return this.remainingCards * 10 + this.step;// - stackedCards /* / 10 * 10*/;
		}

		/**
		 * Generate hash for this state
		 * @return {String}
		 */
		private BigInteger hash() {
			/*string ret = "";
			for (int i = 0; i < trays_count; i++)
				foreach (Card card in this.trays[i])
					ret += card.ToString() + ";";

			foreach (Card card in this.slots)
				ret += "|" + card.ToString();

			return ret; //XXH.h32(ret, 0).toString();*/
			BigInteger hash = new BigInteger();
			for(int i = 0; i < trays_count; i++) {
				List<Card> tray = trays[i];
				foreach(Card card in tray) {
					hash = (hash << Card.HashBits) | card.Hash();
				}
				hash = (hash << Card.HashBits) | (Card.MaxHash + 1);
			}
			foreach(Card card in slots) {
				hash = (hash << Card.HashBits) | card.Hash();
			}
			return hash;
		}

		/**
 * Can this card be stacked?
 * @param {Array} from
 * @param {Array} to
 */
		private static bool can_be_stacked(Card from, Card to) {
			switch (from.Type) {
				case CardType.Dragon: //Dragon cannot be stacked into any other cards
				case CardType.Flower: //Flower cannot too
					return false;
			}
			switch (to.Type) {
				case CardType.Dragon: //Dragon cannot be stacked by any other cards
				case CardType.Flower: //Flower cannot too
					return false;
			}
			if (from.Suit == to.Suit) //if it's same color, it cannot be stacked
				return false;
			return (from.Value + 1 == to.Value); //is it in order?
		}

		public List<Action> Solve() {
			PriorityQueue<State> queue = new PriorityQueue<State>(true, (a, b) => a.priority.CompareTo(b.priority)/*{ if (a.priority > b.priority) return -1; else if (a.priority < b.priority) return 1; else return 0; }*/ ); ;
			List<Card>[] trays = this.trays; //load_from_dom();
			bool isEmpty = true;
			foreach(List<Card> tray in trays) {
				if (tray.Count > 0) {
					isEmpty = false;
					break;
				}
			}
			if (isEmpty){
				return null;
			}
			State currentState = this;
			initialSetup();
			queue.Enqueue(currentState);
			int iteration = 0;
			SortedSet<BigInteger> visitedStates = new SortedSet<BigInteger>();
			visitedStates.Add(currentState.hash());
			while (/*iteration < 50000 &&*/ queue.Count > 0) {
				currentState = queue.Dequeue();
				if (currentState.remainingCards == 0) {
					return get_actions_to_this_state(currentState);
				}
				List<Action> actions = currentState.valid_steps();
				if (verify_unique_state(queue, visitedStates, currentState, actions) == 0) {
					actions = currentState.valid_slot_steps();
					verify_unique_state(queue, visitedStates, currentState, actions);
				}
				if (iteration % 100 == 0) {
					//Console.WriteLine(iteration + " " + visitedStates.Count + " " + queue.Count + " " + currentState.remainingCards, currentState.priority);
				}
				iteration++;
			}
			Console.WriteLine("Oops, unsolveable!");
			return null;
		}

		/**
 * Verify if the actions leads to unique states, returns a number of unique states
 * @param {ProrityQueue} queue 
 * @param {Array} visitedStates 
 * @param {*} actions 
 * @returns int
 */
		private static int verify_unique_state(PriorityQueue<State> queue, SortedSet<BigInteger> visitedStates, State currentState, List<Action> actions) {
			int valid_actions = 0;
			for (int i = 0; i < actions.Count; i++) {
				State newState = new State(currentState, actions[i]);
				BigInteger stateHash = newState.hash();
				if (visitedStates.Contains(stateHash))
					continue;
				valid_actions++;
				queue.Enqueue(newState);
				visitedStates.Add(stateHash);
			}
			return valid_actions;
		}


		/**
 * Print actions to get to this state
 * @param {State} state 
 * @return {Array}
 */
		private static List<Action> get_actions_to_this_state(State state) {
			List<Action> ret = new List<Action>();
			if (state.prevState != null)
				ret = get_actions_to_this_state(state.prevState);
			if (state.action == null) {
				Console.WriteLine(state.step + " " + state.remainingCards);
				return new List<Action>();
			}
			ret.Add(state.action);
		/*
			if ("from" in state.action)
				console.log(state.step, state.remainingCards, state.action.from, state.action.to, state);
			else 
				console.log(state.step, state.remainingCards, state.action.collapse, state);
			*/
			return ret;
		}

	}
}
