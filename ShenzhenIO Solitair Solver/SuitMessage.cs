using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShenzhenIO_Solitair_Solver {
	public partial class SuitMessage : Form {

		public Suit Choice { get; private set; } = Suit.None;

		public SuitMessage(int slotIndex, List<Suit> availableSuits) {
			InitializeComponent();
			this.label1.Text = "Could not autocomplete! Which of the following suits is in slot " + (slotIndex + 1) + "?";
			this.DialogResult = DialogResult.Cancel;
			if (!availableSuits.Contains(Suit.Red)) RedButton.Visible = false;
			if (!availableSuits.Contains(Suit.Green)) GreenBtn.Visible = false;
			if (!availableSuits.Contains(Suit.Black)) BlackButton.Visible = false;
		}

		private void RedButton_Click(object sender, EventArgs e) {
			Choice = Suit.Red;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void GreenBtn_Click(object sender, EventArgs e) {
			Choice = Suit.Green;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void BlackButton_Click(object sender, EventArgs e) {
			Choice = Suit.Black;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void SuitMessage_Load(object sender, EventArgs e) {
			this.Location = this.Owner.Location;
			this.BringToFront();
		}

		private void label1_Click(object sender, EventArgs e) {

		}
	}
}
