using CustomLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public class GameState {

		private Card[] FreeSpaces = new Card[3];
		private bool FlowerSpace = false;
		private Card[] SuitStacks = new Card[3];
		private CustomLinkedList<Card>[] CardStacks = new CustomLinkedList<Card>[8];

		private GameState() {
			for(int i = 0; i < CardStacks.Length; i++) {
				CardStacks[i] = new CustomLinkedList<Card>();
			}
		}

		public GameState(GameState copy) : this() {
			Array.Copy(copy.FreeSpaces, FreeSpaces, FreeSpaces.Length);
			FlowerSpace = copy.FlowerSpace;
			Array.Copy(copy.SuitStacks, SuitStacks, SuitStacks.Length);
			for(int i = 0; i < CardStacks.Length; i++) {
				CardStacks[i] = new CustomLinkedList<Card>(copy.CardStacks[i]);
			}
		}

		public bool CanGrabStack(int stackIndex, int cardIndex) { //From card stack piles
			CustomLinkedList<Card> list = CardStacks[stackIndex];
			if(list.Count > 0) {
				if(cardIndex == list.Count - 1) {
					return true;
				}

				/*IEnumerator<Card> listItems = list.GetEnumerator();
				for(int i = 0; i <= cardIndex; i++) {
					if (listItems.MoveNext() == false) return false;
				}
				SuitType lastSuit = listItems.Current.Suit;
				int lastNumber = listItems.Current.Number;
				while(listItems.MoveNext()) {
					Card card = listItems.Current;
					if (card.IsDragon || card.IsFlower) return false;
					if(card.Suit == lastSuit || card.Number >= lastNumber) {
						return false;
					}
					lastSuit = card.Suit;
					lastNumber = card.Number;
				}*/
				IEnumerable<Card> items = list.Skip(cardIndex);
				SuitType lastSuit = items.First().Suit;
				int lastNumber = items.First().Number;
				foreach (Card card in items.Skip(1)) {
					if (card.IsDragon || card.IsFlower) return false;
					if (card.Suit == lastSuit || card.Number >= lastNumber) {
						return false;
					}
					lastSuit = card.Suit;
					lastNumber = card.Number;
				}
				return true;
			}

			return false;
		}

		private CustomLinkedList<Card> GrabCard(MoveType location, int index, int cardIndex) {
			if(location == MoveType.FreeSpace) {
				if(!FreeSpaces[index].IsEmpty) {
					Card card = FreeSpaces[index];
					FreeSpaces[index] = Card.EmptyCard;
					CustomLinkedList<Card> result = new CustomLinkedList<Card>();
					result.Add(card);
					return result;
				}
			}else if(location == MoveType.CardStack) {
				if(CanGrabStack(index, cardIndex)) {
					Node<Card> node = CardStacks[index].NodeAt(cardIndex);
					return CardStacks[index].RemoveRange(node);
				}
			}

			throw new NullReferenceException("Invalid move.");
		}

		public void Play(Move move) {
			if (move.MoveType == MoveType.StackDragons) {
				//Special dragon move
				int collectedCards = 0;
				for(int i = 0; i < FreeSpaces.Length; i++) {
					if(FreeSpaces[i].IsDragon && FreeSpaces[i].Suit == move.FromCard.Suit) {
						collectedCards++;
						if(i == move.FromIndex) {
							//TODO can't use space
							throw new NotImplementedException();
						} else {
							FreeSpaces[i] = Card.EmptyCard;
						}
					}
				}
				foreach(CustomLinkedList<Card> stack in CardStacks) {
					Card last = stack.Last.Value;
					if(last.IsDragon && last.Suit == move.FromCard.Suit) {
						collectedCards++;
						stack.RemoveLast();
					}
				}
				if (collectedCards != 4) throw new InvalidOperationException("Not enough dragon cards found.");
				return;
			}

			CustomLinkedList<Card> pickup = GrabCard(move.FromLocation, move.FromIndex, move.FromCardIndex);
			if (pickup.Count > 0) {
				if (move.MoveType == MoveType.FreeSpace) {
					if (pickup.Count == 1 && !pickup.First.Value.IsFlower && FreeSpaces[move.ToIndex].IsEmpty) {
						FreeSpaces[move.ToIndex] = pickup.First.Value;
						return;
					}
				} else if (move.MoveType == MoveType.SuitStack) {
					if (pickup.Count == 1) {
						Card card = pickup.First.Value;
						Card stack = SuitStacks[move.ToIndex];
						if (!card.IsDragon && !card.IsFlower && (stack.IsEmpty || (card.Suit == stack.Suit && card.Number == (stack.Number + 1)))) {
							SuitStacks[move.ToIndex] = card;
							return;
						}
					}
				} else if (move.MoveType == MoveType.CardStack) {
					Card card = pickup.First.Value;
					CustomLinkedList<Card> stack = CardStacks[move.ToIndex];
					Card last = stack.Last.Value;
					if (!card.IsFlower && (stack.Count == 0 || (!last.IsDragon && !last.IsFlower && last.Suit != card.Suit && card.Number == last.Number - 1))) {
						stack.Add(pickup);
					}
				} else if (move.MoveType == MoveType.Flower) {
					if (pickup.Count == 1 && pickup.First.Value.IsFlower && !FlowerSpace) {
						FlowerSpace = true;
						return;
					}
				}
			}

			throw new InvalidOperationException("Invalid move");
		}

		public void Undo(Move move) {
			if (move.MoveType == MoveType.StackDragons) {
				//Special dragon move
				int collectedCards = 0;
				for (int i = 0; i < FreeSpaces.Length; i++) {
					if (FreeSpaces[i].IsDragon && FreeSpaces[i].Suit == move.FromCard.Suit) {
						collectedCards++;
						if (i == move.FromIndex) {
							//TODO can't use space
							throw new NotImplementedException();
						} else {
							FreeSpaces[i] = Card.EmptyCard;
						}
					}
				}
				foreach (CustomLinkedList<Card> stack in CardStacks) {
					Card last = stack.Last.Value;
					if (last.IsDragon && last.Suit == move.FromCard.Suit) {
						collectedCards++;
						stack.RemoveLast();
					}
				}
				if (collectedCards != 4) throw new InvalidOperationException("Not enough dragon cards found.");
				return;
			}

			CustomLinkedList<Card> pickup = GrabCard(move.FromLocation, move.FromIndex, move.FromCardIndex);
			if (pickup.Count > 0) {
				if (move.MoveType == MoveType.FreeSpace) {
					if (pickup.Count == 1 && !pickup.First.Value.IsFlower && FreeSpaces[move.ToIndex].IsEmpty) {
						FreeSpaces[move.ToIndex] = pickup.First.Value;
						return;
					}
				} else if (move.MoveType == MoveType.SuitStack) {
					if (pickup.Count == 1) {
						Card card = pickup.First.Value;
						Card stack = SuitStacks[move.ToIndex];
						if (!card.IsDragon && !card.IsFlower && (stack.IsEmpty || (card.Suit == stack.Suit && card.Number == (stack.Number + 1)))) {
							SuitStacks[move.ToIndex] = card;
							return;
						}
					}
				} else if (move.MoveType == MoveType.CardStack) {
					Card card = pickup.First.Value;
					CustomLinkedList<Card> stack = CardStacks[move.ToIndex];
					Card last = stack.Last.Value;
					if (!card.IsFlower && (stack.Count == 0 || (!last.IsDragon && !last.IsFlower && last.Suit != card.Suit && card.Number == last.Number - 1))) {
						stack.Add(pickup);
					}
				} else if (move.MoveType == MoveType.Flower) {
					if (pickup.Count == 1 && pickup.First.Value.IsFlower && !FlowerSpace) {
						FlowerSpace = true;
						return;
					}
				}
			}

			throw new InvalidOperationException("Invalid move");
		}

		private bool SuitIsInSuitStack(SuitType suit) {
			for(int i = 0; i < 3; i++) {
				Card card = SuitStacks[i];
				if(!card.IsEmpty && card.Suit == suit) {
					return true;
				}
			}
			return false;
		}

		public List<Move> GetPossibleMoves() {
			List<Move> moves = new List<Move>();

			//Check cards in free spaces
			for(int i = 0; i < FreeSpaces.Length; i++) {
				if (!FreeSpaces[i].IsEmpty) {
					Card grab = FreeSpaces[i];

					//Check each suit stack
					if (!grab.IsDragon) {
						for (int j = 0; j < SuitStacks.Length; j++) {
							if (SuitStacks[j].IsEmpty) {
								if (!SuitIsInSuitStack(grab.Suit)) {
									moves.Add(new Move(MoveType.FreeSpace, i, -1, grab, MoveType.SuitStack, MoveType.SuitStack, j, SuitStacks[j]));
								}
							} else {
								if (SuitStacks[j].Suit == grab.Suit && SuitStacks[j].Number == grab.Number - 1) {
									moves.Add(new Move(MoveType.FreeSpace, i, -1, grab, MoveType.SuitStack, MoveType.SuitStack, j, SuitStacks[j]));
								}
							}
						}
					}

					//Check each card stack
					for (int j = 0; j < CardStacks.Length; j++) {
						CustomLinkedList<Card> list = CardStacks[j];
						if(CanDropOnStack(grab, list.Last.Value)) {
							moves.Add(new Move(MoveType.FreeSpace, i, -1, grab, MoveType.CardStack, MoveType.CardStack, j, list.Last.Value));
						}
					}
				}
			}

			//Check cards on the card stack
			for(int i = 0; i < CardStacks.Length; i++) {
				if(CardStacks[i].Count > 0) {
					Node<Card> node = CardStacks[i].Last;
					int x = CardStacks[i].Count - 1;

					while(node != null) {
						Card grab = node.Value;

						//Check for flower
						if (grab.IsFlower) {
							moves.Add(new Move(MoveType.CardStack, i, x, grab, MoveType.Flower, MoveType.Flower, -1, Card.EmptyCard));
							break;
						}

						//Check each suit stack
						if (!grab.IsDragon) {
							for (int j = 0; j < SuitStacks.Length; j++) {
								if (SuitStacks[j].IsEmpty) {
									if (!SuitIsInSuitStack(grab.Suit)) {
										moves.Add(new Move(MoveType.CardStack, i, x, grab, MoveType.SuitStack, MoveType.SuitStack, j, SuitStacks[j]));
									}
								} else {
									if (SuitStacks[j].Suit == grab.Suit && SuitStacks[j].Number == grab.Number - 1) {
										moves.Add(new Move(MoveType.CardStack, i, x, grab, MoveType.SuitStack, MoveType.SuitStack, j, SuitStacks[j]));
									}
								}
							}
						}

						//Check each free space
						for(int j = 0; j < FreeSpaces.Length; j++) {
							if (FreeSpaces[j].IsEmpty) {
								moves.Add(new Move(MoveType.CardStack, i, x, grab, MoveType.FreeSpace, MoveType.FreeSpace, j, FreeSpaces[j]));
							}
						}

						//TODO Check if can grab next card!
						if(node.Previous.Value.IsDragon || node.Previous.Value.IsFlower || node.Previous.Value.Suit != grab.Suit || node.Previous.Value.Number != grab.Number + 1) {
							break;
						}
						node = node.Previous;
						x--;
					}
				}
			}

			return moves;
		}

		private bool CanDropOnStack(Card dragging, Card dropOn) {
			if (dropOn.IsEmpty) return true;
			if (dropOn.IsDragon || dropOn.IsFlower || dragging.IsDragon || dragging.IsFlower) return false;
			return (dragging.Suit != dropOn.Suit) && (dragging.Number == dropOn.Number - 1);
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			foreach (Card card in FreeSpaces) sb.Append(card.ToString().PadRight(3));
			sb.Append("   ");
			sb.Append(FlowerSpace ? "F  " : "   ");
			foreach (Card card in SuitStacks) sb.Append(card.ToString().PadRight(3));
			sb.AppendLine();

			Card[][] stacks = new Card[CardStacks.Length][];
			for (int i = 0; i < CardStacks.Length; i++) stacks[i] = CardStacks[i].Reverse().ToArray();

			int rowIndex = 0;
			while (true) {
				string rowString = "";
				bool containedCard = false;
				for(int i = 0; i < stacks.Length; i++) {
					if(rowIndex < stacks[i].Length) {
						rowString += stacks[i][rowIndex].ToString().PadRight(3);
						containedCard = true;
					} else {
						rowString += "   ";
					}
				}

				if (!containedCard) break;
				else sb.AppendLine(rowString);
				rowIndex++;
			}

			return sb.ToString();
		}

		public bool IsValidGameState() {
			//Check for duplicate suits in suit stack, numbers will be chacked later
			List<SuitType> suits = new List<SuitType>();
			foreach (SuitType suit in SuitStacks.Select(x => x.Suit).Where(x => x != SuitType.Empty)) {
				if (suits.Contains(suit)) {
					return false;
				} else {
					suits.Add(suit);
				}
			}

			bool Flower = FlowerSpace;
			Dictionary<SuitType, int> dragons = new Dictionary<SuitType, int>();
			Dictionary<SuitType, bool[]> cards = new Dictionary<SuitType, bool[]>();
			SuitType[] allSuits = new SuitType[] { SuitType.Black, SuitType.Green, SuitType.Red };
			foreach (SuitType suit in allSuits) {
				cards[suit] = new bool[9]; //1-9
				dragons[suit] = 0;
			}

			foreach(Card card in SuitStacks.Where(x => !x.IsEmpty)) {
				if (card.IsDragon || card.IsFlower) return false;
				for(int i = 1; i <= card.Number; i++) {
					if (cards[card.Suit][i - 1] == true) return false;
					cards[card.Suit][i - 1] = true;
				}
			}
			
			IEnumerable<Card> allCards = CardStacks.SelectMany(x => x).Concat(FreeSpaces).Where(x => !x.IsEmpty);
			foreach(Card card in allCards) {
				if (card.IsFlower) {
					if (Flower) return false;
					Flower = true;
				} else if (card.IsDragon) {
					if (dragons[card.Suit] >= 4) return false;
					dragons[card.Suit] = dragons[card.Suit] + 1;
				} else { 
					int number = card.Number - 1;
					if (cards[card.Suit][card.Number - 1] == true) return false;
					cards[card.Suit][card.Number - 1] = true;
				}
			}

			//Check every card is accounted for
			foreach(SuitType suit in allSuits) {
				if (dragons[suit] != 4) return false;
				for(int i = 1; i <= 9; i++) {
					if (cards[suit][i - 1] == false) return false;
				}
			}

			return Flower;
		}

		#region Parsing
		private bool TryTopRowParse(string input) {
			string[] components = input.Split(' ');
			if (components.Length != 7) return false;

			for(int i = 0; i < 3; i++) {
				if(!Card.TryParse(components[i], out FreeSpaces[i])) {
					return false;
				}
			}

			Card flowerSpot;
			if (!Card.TryParse(components[3], out flowerSpot)) return false;
			if (flowerSpot.IsFlower) FlowerSpace = true;
			else if (!flowerSpot.IsEmpty) return false;

			for(int i = 0; i < 3; i++) {
				if(!Card.TryParse(components[i + 4], out SuitStacks[i])) {
					return false;
				}
			}

			//Check for duplicate suits in suit stack, numbers will be chacked later
			List<SuitType> suits = new List<SuitType>();
			/*for(int i = 0; i < 3; i++) {
				if (!SuitStacks[i].IsEmpty) {
					if (suits.Contains(SuitStacks[i].Suit)) {
						return false;
					} else {
						suits.Add(SuitStacks[i].Suit);
					}
				}
			}*/
			foreach(SuitType suit in SuitStacks.Select(x => x.Suit).Where(x => x != SuitType.Empty)) {
				if (suits.Contains(suit)) {
					return false;
				} else {
					suits.Add(suit);
				}
			}

			return true;
		}

		private bool TryRowParse(int row, string input) {
			string[] components = input.Split(' ');
			if (components.Length != CardStacks.Length) return false;
			
			for(int col = 0; col < CardStacks.Length; col++) {
				Card card;
				if (!Card.TryParse(components[col], out card)) return false;
				if (!card.IsEmpty) {
					if (CardStacks[col].Count != (row - 1)) return false;
					CardStacks[col].Add(card);
				}
			}

			return true;
		}

		public static GameState RequestUserInput() {
			GameState game;
			Console.Clear();
			while (true) {
				game = new GameState();
				string legend = "Space separated, Numbers = 1 - 9, Suit = B, R, G, Dragon = D, Flower = F, Empty = -(EX--B1 - RD - F )";
				string input;

				while (true) {
					Console.WriteLine("Enter top row (3 free spaces, 1 flower, 3 suit stacks): " + legend);
					input = Console.ReadLine();
					if (game.TryTopRowParse(input)) {
						break;
					}
					Console.Clear();
					Console.WriteLine("Bad input.");
				}

				for (int i = 1; i <= 5; i++) {
					Console.Clear();
					while (true) {
						Console.WriteLine("Enter row " + i + ": " + legend);
						input = Console.ReadLine();
						if (game.TryRowParse(i, input)) {
							break;
						}
						Console.Clear();
						Console.WriteLine("Bad input.");
					}
				}

				Console.Clear();
				if (game.IsValidGameState()) {
					break;
				} else {
					Console.WriteLine("Invalid game state.");
				}
			}
			return game;
		}
		#endregion

	}
}
