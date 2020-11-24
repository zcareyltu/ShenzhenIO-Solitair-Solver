using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShenzhenIO_Solitair_Solver {
	public partial class Form1 : Form {

		private const int ActionWaitTime = 750;

		private int Tray1X;
		private int Tray8X;

		private int Card1Y;
		private int Card2Y;

		private State initialState = new State();

		public Form1() {
			InitializeComponent();

			Tray1X = decimal.ToInt32(Tray1XAdjustment.Value);
			Tray8X = decimal.ToInt32(Tray8XAdjustment.Value);

			Card1Y = decimal.ToInt32(Card1YAdjustment.Value);
			Card2Y = decimal.ToInt32(Card2YAdjustment.Value);

			int tabIndex = 8;
			for(int y = 0; y < 5; y++) {
				for(int x = 0; x < 8; x++) {
					this.Controls.Find("Card" + x + y, false)[0].TabIndex = tabIndex++;
				}
			}
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

		private bool ParseState() {
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
		}

		private void processSuitStack(string text, int index) {
			if(text.Length > 0 && text != "Empty") {
				if (text == "Green") {
					initialState.SetSuitStack(Suit.Green, index);
				} else if (text == "Red") {
					initialState.SetSuitStack(Suit.Red, index);
				}else if(text == "Black") {
					initialState.SetSuitStack(Suit.Black, index);
				}
			}
		}


		private void SolveButton_Click(object sender, EventArgs e) {
			if (ParseState()) {
				List<Action> actions = initialState.Solve();
				if (actions != null && actions.Count > 0) {
					MessageBox.Show("Press 'Enter' on the keyboard to solve automatically. DO NOT TOUCH YOUR MOUSE.");
					AutoClick(actions);
				} else {
					MessageBox.Show("Failed to solve.");
				}
			}
			initialState = new State(); //Clear out our memory
		}

		private void ClearButton_Click(object sender, EventArgs e) {
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 8; x++) {
					this.Controls.Find("Card" + x + y, false)[0].Text = "";
				}
			}
			this.initialState = new State();
		}

		private void AutoClick(List<Action> actions) {
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);

			//Get focus of the game
			mouse.MoveToFreeSpace(0);
			mouse.ShortClick();

			foreach(Action action in actions) {
				if(action.Collapse != null) {
					Suit target = (Suit)action.Collapse;
					if (target == Suit.Red) mouse.MoveToDragonButton(0);
					else if (target == Suit.Green) mouse.MoveToDragonButton(1);
					else if (target == Suit.Black) mouse.MoveToDragonButton(2);
					mouse.LongClick(ActionWaitTime);
				}else if(action.Pop != null) {
					mouse.MoveTo((int)action.Pop, (int)action.PopCardIndex);
					mouse.ClickAndHold();
					mouse.MoveToSuitSpace((int)action.PopToStackIndex);
					mouse.Release(ActionWaitTime);
				} else {
					if(action.From != null) {
						mouse.MoveTo((int)action.From, (int)action.FromCardIndex);
						mouse.ClickAndHold();
					} else {
						mouse.MoveToFreeSpace((int)action.FromSlot);
						mouse.ClickAndHold();
					}
					if(action.To != null) {
						mouse.MoveTo((int)action.To, (int)action.ToCardIndex);
						mouse.Release(ActionWaitTime);
					} else {
						mouse.MoveToFreeSpace((int)action.ToSlot);
						mouse.Release(ActionWaitTime);
					}
				}
			}
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
