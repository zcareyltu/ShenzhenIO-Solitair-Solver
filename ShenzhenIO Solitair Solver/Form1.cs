using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShenzhenIO_Solitair_Solver {
	public partial class Form1 : Form {

		private const int ClickTime = 200;

		private const int ActionWaitTime = 200;
		private const int MoveWaitTime = 200;
		private const int CollapseWaitTime = 400;

		private const int WaitTimePerCard = 400;
		

		private int Tray1X;
		private int Tray8X;

		private int Card1Y;
		private int Card2Y;

		//private State initialState = new State();
		private Dictionary<Suit, int> suitStacks = new Dictionary<Suit, int>();

		public Form1() {
			InitializeComponent();

			Tray1X = decimal.ToInt32(Tray1XAdjustment.Value);
			Tray8X = decimal.ToInt32(Tray8XAdjustment.Value);

			Card1Y = decimal.ToInt32(Card1YAdjustment.Value);
			Card2Y = decimal.ToInt32(Card2YAdjustment.Value);

			/*int tabIndex = 8;
			for(int y = 0; y < 5; y++) {
				for(int x = 0; x < 8; x++) {
					this.Controls.Find("Card" + x + y, false)[0].TabIndex = tabIndex++;
				}
			}*/
		}

		private void Tray1XAdjustment_ValueChanged(object sender, EventArgs e) {
			Tray1X = decimal.ToInt32(Tray1XAdjustment.Value);
			Cursor.Position = new Point(Tray1X, Cursor.Position.Y);
		}

		private void Tray8XAdjustment_ValueChanged(object sender, EventArgs e) {
			Tray8X = decimal.ToInt32(Tray8XAdjustment.Value);
			Cursor.Position = new Point(Tray8X, Cursor.Position.Y);
		}

		private void Card1YAdjustment_ValueChanged(object sender, EventArgs e) {
			Card1Y = decimal.ToInt32(Card1YAdjustment.Value);
			Cursor.Position = new Point(Cursor.Position.X, Card1Y);
		}

		private void Card2YAdjustment_ValueChanged(object sender, EventArgs e) {
			Card2Y = decimal.ToInt32(Card2YAdjustment.Value);
			Cursor.Position = new Point(Cursor.Position.X, Card2Y);
		}

		private void TestBtn_Click(object sender, EventArgs e) {
			int tray = decimal.ToInt32(TraySelector.Value);
			int card = decimal.ToInt32(CardSelector.Value);
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);

			if(tray == 0) {
				if(card >= 1 && card <= 3) {
					mouse.MoveToFreeSpace(card - 1);
				}else if(card == 4) {
					mouse.MoveToFlowerSpace();
				}else if(card >= 5 && card <= 7) {
					mouse.MoveToSuitSpace(card - 5);
				}
			} else {
				mouse.MoveTo(tray - 1, card - 1);
			}
		}

		private void TestDragonBtn_Click(object sender, EventArgs e) {
			int btn = decimal.ToInt32(DragonButtonSelector.Value);
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);

			if (btn >= 1 && btn <= 3) {
				mouse.MoveToDragonButton(btn - 1);
			}
		}

		private void Card_TextChanged(object sender, EventArgs e) {
		/*	if(sender is TextBox card) {
				if (card.Name.StartsWith("Card")) {
					string txt = card.Text;
					Card result;
					if(Card.TryParse(txt, out result)) {
						int x = int.Parse(card.Name[4].ToString());
						int y = int.Parse(card.Name[5].ToString());

						initialState.SetCard(x, y, result);
					} else {
						MessageBox.Show("Invalid card name.");
						card.TextChanged -= Card_TextChanged;
						card.Text = "";
						card.TextChanged += Card_TextChanged;
					}
				}
			}*/
		}

		/*private bool ParseState() {
			string loadString = "";
			initialState = new State();
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 8; x++) {
					TextBox card = (TextBox)this.Controls.Find("Card" + x + y, false)[0];
					string txt = card.Text;
					Card result;
					if (Card.TryParse(txt, out result)) {
						//int x = int.Parse(card.Name[4].ToString());
						//int y = int.Parse(card.Name[5].ToString());
						loadString += result.ToString() + ";";
						initialState.SetCard(x, y, result);
					} else {
						MessageBox.Show("Invalid card name.");
						card.TextChanged -= Card_TextChanged;
						card.Text = "";
						card.TextChanged += Card_TextChanged;
						return false;
					}
				}
			}
			initialState.CleanTrays();
			loadString = loadString.TrimEnd(';');
			Console.WriteLine(loadString);
			Console.Out.Flush();
			LoadTextBox.Text = loadString;
			processSuitStack(SuitStack0.Text, 0);
			processSuitStack(SuitStack1.Text, 1);
			processSuitStack(SuitStack2.Text, 2);
			return true;
		}*/

		private void processSuitStack(string text, int index) {
			if(text.Length > 0 && text != "Empty") {
				if (text == "Green") {
					suitStacks[Suit.Green] = index; //initialState.SetSuitStack(Suit.Green, index);
				} else if (text == "Red") {
					suitStacks[Suit.Red] = index; //initialState.SetSuitStack(Suit.Red, index);
				}else if(text == "Black") {
					suitStacks[Suit.Black] = index; //initialState.SetSuitStack(Suit.Black, index);
				}
			}
		}


		private void SolveButton_Click(object sender, EventArgs e) {
			bool debug = DebugBox.Checked;
			suitStacks = new Dictionary<Suit, int>();
			processSuitStack(SuitStack0.Text, 0);
			processSuitStack(SuitStack1.Text, 1);
			processSuitStack(SuitStack2.Text, 2);
			//if (ParseState()) {
				List<Action> actions = Action.ParseWebCommands(LoadTextBox.Text); //initialState.Solve();
				if (actions != null && actions.Count > 0) {
					DialogResult result = MessageBox.Show("Press 'Enter' on the keyboard to solve automatically. DO NOT TOUCH YOUR MOUSE.", "Message", MessageBoxButtons.OKCancel);
					if (result == DialogResult.OK) {
						AutoClick(actions, debug);
						ClearButton_Click(null, null);
					}
				} else {
					MessageBox.Show("Failed to solve.");
				}
			//}
			//initialState = new State(); //Clear out our memory
		}

		private void ClearButton_Click(object sender, EventArgs e) {
			/*for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 8; x++) {
					this.Controls.Find("Card" + x + y, false)[0].Text = "";
				}
			}
			this.initialState = new State();*/
			this.LoadTextBox.Text = "";
			SuitStack0.Text = "";
			SuitStack1.Text = "";
			SuitStack2.Text = "";
		}

		private int getSuitStackIndex(Suit suit) {
			if (suitStacks.ContainsKey(suit)) {
				return suitStacks[suit];
			} else {
				for(int i = 0; i < 3; i++) {
					if (!suitStacks.ContainsValue(i)) {
						suitStacks[suit] = i;
						return i;
					}
				}
				throw new InvalidOperationException("Could not find suit index.");
			}
		}

		private void updateSuitStackLabel(ComboBox label, Suit suit) {
			if (label.Text != suit.ToString()) {
				label.Text = suit.ToString();
				label.Refresh();
			}
		}

		private void updateSuitStackLabels() {
			foreach(KeyValuePair<Suit, int> pair in suitStacks) {
				if (pair.Value == 0) updateSuitStackLabel(SuitStack0, pair.Key);
				else if (pair.Value == 1) updateSuitStackLabel(SuitStack1, pair.Key);
				else if (pair.Value == 2) updateSuitStackLabel(SuitStack2, pair.Key);
			}
		}

		private void AutoClick(List<Action> actions, bool debug = false) {
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);

			//Get focus of the game
			mouse.MoveToFreeSpace(0);
			mouse.ShortClick(ClickTime);

			foreach(Action action in actions) {
				if (debug) {
					MessageBox.Show(action.ToString());
					mouse.MoveToFreeSpace(0);
					mouse.ShortClick(ClickTime);
				}
				if (action.Collapse != null) {
					Suit target = (Suit)action.Collapse;
					if (target == Suit.Red) mouse.MoveToDragonButton(0, MoveWaitTime);
					else if (target == Suit.Green) mouse.MoveToDragonButton(1, MoveWaitTime);
					else if (target == Suit.Black) mouse.MoveToDragonButton(2, MoveWaitTime);
					mouse.LongClick(CollapseWaitTime, ClickTime);
				} else if (action.Pop != null) {
					mouse.MoveTo((int)action.Pop, (int)action.PopCardIndex, MoveWaitTime);
					mouse.ClickAndHold(ClickTime);
					mouse.MoveToSuitSpace(getSuitStackIndex((Suit)action.PopSuit), MoveWaitTime);
					mouse.Release(ActionWaitTime);
				}else if(action.WaitCount != null) {
					foreach(Suit suit in action.WaitSuitOrder) {
						getSuitStackIndex(suit); //Will add the suit to the first available slot if not already stored.
					}
					updateSuitStackLabels();
					Thread.Sleep(WaitTimePerCard * (int)action.WaitCount);
					/*if (checkSuitStacks()) {
						mouse.MoveToFreeSpace(0);
						mouse.ShortClick(ClickTime);
					}*/
				} else {
					if(action.From != null) {
						mouse.MoveTo((int)action.From, (int)action.FromCardIndex, MoveWaitTime);
						mouse.ClickAndHold(ClickTime);
					} else {
						mouse.MoveToFreeSpace((int)action.FromSlot, MoveWaitTime);
						mouse.ClickAndHold(ClickTime);
					}
					if(action.To != null) {
						mouse.MoveTo((int)action.To, (int)action.ToCardIndex, MoveWaitTime);
						mouse.Release(ActionWaitTime);
					} else {
						mouse.MoveToFreeSpace((int)action.ToSlot, MoveWaitTime);
						mouse.Release(ActionWaitTime);
					}
				}
			}
		}

		private bool checkSuitStacks() {
			int index = -1;
			for(int i = 0; i < 3; i++) {
				if (!suitStacks.ContainsValue(i)) {
					index = i;
					break;
				}
			}

			if (index >= 0) {
				List<Suit> remainingSuits = ((Suit[])Enum.GetValues(typeof(Suit))).Where(x => x != Suit.None && !suitStacks.ContainsKey(x)).ToList();
				if(remainingSuits.Count == 1) {
					suitStacks[remainingSuits[0]] = index;
					return false;
				}
				SuitMessage message = new SuitMessage(index, remainingSuits);
				message.Owner = this;
				if(message.ShowDialog() == DialogResult.OK && !suitStacks.ContainsKey(message.Choice)) {
					suitStacks[message.Choice] = index;
				}
				return true;
			}

			return false;
		}

		private void LoadButton_Click(object sender, EventArgs e) {
			string loadString = LoadTextBox.Text;
			string[] cards = loadString.Split(';');
			if(cards.Length != 40) {
				MessageBox.Show("Card count must be 40, found " + cards.Length);
				ClearButton_Click(null, null);
				return;
			}
			for(int y = 0; y < 5; y++) {
				for(int x = 0; x < 8; x++) {
					Card card = new Card();
					if(Card.TryParse(cards[y * 8 + x], out card)) {
						((TextBox)this.Controls.Find("Card" + x + y, false)[0]).Text = card.IsEmpty ? "" : card.ToString();
					} else {
						MessageBox.Show("Could not parse card: \'" + cards[y * 8 + x] + "\'");
						ClearButton_Click(null, null);
						return;
					}
				}
			}

		}

		/*private void button1_Click(object sender, EventArgs e) {
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);
			mouse.MoveToFreeSpace(0);
			mouse.ShortClick();
			mouse.MoveTo(7, 4);
			mouse.ClickAndHold();
			mouse.MoveToFreeSpace(2);
			mouse.Release();
		}*/
	}
}
