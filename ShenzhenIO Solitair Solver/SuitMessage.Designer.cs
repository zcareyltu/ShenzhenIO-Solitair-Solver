namespace ShenzhenIO_Solitair_Solver {
	partial class SuitMessage {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.RedButton = new System.Windows.Forms.Button();
			this.GreenBtn = new System.Windows.Forms.Button();
			this.BlackButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(351, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Could not autocomplete! Which of the following suits is in slot 2?";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// RedButton
			// 
			this.RedButton.Location = new System.Drawing.Point(12, 60);
			this.RedButton.Name = "RedButton";
			this.RedButton.Size = new System.Drawing.Size(75, 23);
			this.RedButton.TabIndex = 1;
			this.RedButton.Text = "Red";
			this.RedButton.UseVisualStyleBackColor = true;
			this.RedButton.Click += new System.EventHandler(this.RedButton_Click);
			// 
			// GreenBtn
			// 
			this.GreenBtn.Location = new System.Drawing.Point(132, 60);
			this.GreenBtn.Name = "GreenBtn";
			this.GreenBtn.Size = new System.Drawing.Size(75, 23);
			this.GreenBtn.TabIndex = 1;
			this.GreenBtn.Text = "Green";
			this.GreenBtn.UseVisualStyleBackColor = true;
			this.GreenBtn.Click += new System.EventHandler(this.GreenBtn_Click);
			// 
			// BlackButton
			// 
			this.BlackButton.Location = new System.Drawing.Point(257, 60);
			this.BlackButton.Name = "BlackButton";
			this.BlackButton.Size = new System.Drawing.Size(75, 23);
			this.BlackButton.TabIndex = 1;
			this.BlackButton.Text = "Black";
			this.BlackButton.UseVisualStyleBackColor = true;
			this.BlackButton.Click += new System.EventHandler(this.BlackButton_Click);
			// 
			// SuitMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 126);
			this.Controls.Add(this.BlackButton);
			this.Controls.Add(this.GreenBtn);
			this.Controls.Add(this.RedButton);
			this.Controls.Add(this.label1);
			this.Name = "SuitMessage";
			this.Text = "SuitMessage";
			this.Load += new System.EventHandler(this.SuitMessage_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RedButton;
		private System.Windows.Forms.Button GreenBtn;
		private System.Windows.Forms.Button BlackButton;
	}
}