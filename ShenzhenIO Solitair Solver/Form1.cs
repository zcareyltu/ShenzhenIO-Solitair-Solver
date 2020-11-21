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

		private int Tray1X;
		private int Tray8X;

		private int Card1Y;
		private int Card2Y;

		public Form1() {
			InitializeComponent();

			Tray1X = decimal.ToInt32(Tray1XAdjustment.Value);
			Tray8X = decimal.ToInt32(Tray8XAdjustment.Value);

			Card1Y = decimal.ToInt32(Card1YAdjustment.Value);
			Card2Y = decimal.ToInt32(Card2YAdjustment.Value);
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

		private void button1_Click(object sender, EventArgs e) {
			MouseManager mouse = new MouseManager(Tray1X, Tray8X, Card1Y, Card2Y);
			mouse.MoveToFreeSpace(0);
			mouse.ShortClick();
			mouse.MoveTo(7, 4);
			mouse.ClickAndHold();
			mouse.MoveToFreeSpace(2);
			mouse.Release();
		}
	}
}
