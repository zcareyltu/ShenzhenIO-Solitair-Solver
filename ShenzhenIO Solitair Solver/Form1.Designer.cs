namespace ShenzhenIO_Solitair_Solver {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.Tray1XAdjustment = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.Card1YAdjustment = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.Tray8XAdjustment = new System.Windows.Forms.NumericUpDown();
			this.Card2YAdjustment = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.TraySelector = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.CardSelector = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.TestBtn = new System.Windows.Forms.Button();
			this.SolveButton = new System.Windows.Forms.Button();
			this.ClearButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.DragonButtonSelector = new System.Windows.Forms.NumericUpDown();
			this.TestDragonBtn = new System.Windows.Forms.Button();
			this.LoadTextBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.SuitStack0 = new System.Windows.Forms.ComboBox();
			this.SuitStack1 = new System.Windows.Forms.ComboBox();
			this.SuitStack2 = new System.Windows.Forms.ComboBox();
			this.DebugBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.Tray1XAdjustment)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Card1YAdjustment)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Tray8XAdjustment)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Card2YAdjustment)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TraySelector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CardSelector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DragonButtonSelector)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tray 1 X: ";
			// 
			// Tray1XAdjustment
			// 
			this.Tray1XAdjustment.Location = new System.Drawing.Point(71, 12);
			this.Tray1XAdjustment.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.Tray1XAdjustment.Name = "Tray1XAdjustment";
			this.Tray1XAdjustment.Size = new System.Drawing.Size(120, 23);
			this.Tray1XAdjustment.TabIndex = 1;
			this.Tray1XAdjustment.Value = new decimal(new int[] {
            404,
            0,
            0,
            0});
			this.Tray1XAdjustment.ValueChanged += new System.EventHandler(this.Tray1XAdjustment_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Card 1 Y: ";
			// 
			// Card1YAdjustment
			// 
			this.Card1YAdjustment.Location = new System.Drawing.Point(71, 94);
			this.Card1YAdjustment.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.Card1YAdjustment.Name = "Card1YAdjustment";
			this.Card1YAdjustment.Size = new System.Drawing.Size(120, 23);
			this.Card1YAdjustment.TabIndex = 1;
			this.Card1YAdjustment.Value = new decimal(new int[] {
            391,
            0,
            0,
            0});
			this.Card1YAdjustment.ValueChanged += new System.EventHandler(this.Card1YAdjustment_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Tray 8 X: ";
			// 
			// Tray8XAdjustment
			// 
			this.Tray8XAdjustment.Location = new System.Drawing.Point(71, 41);
			this.Tray8XAdjustment.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.Tray8XAdjustment.Name = "Tray8XAdjustment";
			this.Tray8XAdjustment.Size = new System.Drawing.Size(120, 23);
			this.Tray8XAdjustment.TabIndex = 1;
			this.Tray8XAdjustment.Value = new decimal(new int[] {
            1468,
            0,
            0,
            0});
			this.Tray8XAdjustment.ValueChanged += new System.EventHandler(this.Tray8XAdjustment_ValueChanged);
			// 
			// Card2YAdjustment
			// 
			this.Card2YAdjustment.Location = new System.Drawing.Point(71, 123);
			this.Card2YAdjustment.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.Card2YAdjustment.Name = "Card2YAdjustment";
			this.Card2YAdjustment.Size = new System.Drawing.Size(120, 23);
			this.Card2YAdjustment.TabIndex = 1;
			this.Card2YAdjustment.Value = new decimal(new int[] {
            422,
            0,
            0,
            0});
			this.Card2YAdjustment.ValueChanged += new System.EventHandler(this.Card2YAdjustment_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Card 2 Y: ";
			// 
			// TraySelector
			// 
			this.TraySelector.Location = new System.Drawing.Point(52, 213);
			this.TraySelector.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.TraySelector.Name = "TraySelector";
			this.TraySelector.Size = new System.Drawing.Size(120, 23);
			this.TraySelector.TabIndex = 2;
			this.TraySelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 215);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 15);
			this.label5.TabIndex = 3;
			this.label5.Text = "Tray: ";
			// 
			// CardSelector
			// 
			this.CardSelector.Location = new System.Drawing.Point(52, 242);
			this.CardSelector.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.CardSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CardSelector.Name = "CardSelector";
			this.CardSelector.Size = new System.Drawing.Size(120, 23);
			this.CardSelector.TabIndex = 2;
			this.CardSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 244);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 15);
			this.label6.TabIndex = 3;
			this.label6.Text = "Card: ";
			// 
			// TestBtn
			// 
			this.TestBtn.Location = new System.Drawing.Point(12, 271);
			this.TestBtn.Name = "TestBtn";
			this.TestBtn.Size = new System.Drawing.Size(80, 32);
			this.TestBtn.TabIndex = 4;
			this.TestBtn.Text = "Test";
			this.TestBtn.UseVisualStyleBackColor = true;
			this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
			// 
			// SolveButton
			// 
			this.SolveButton.Location = new System.Drawing.Point(342, 242);
			this.SolveButton.Name = "SolveButton";
			this.SolveButton.Size = new System.Drawing.Size(77, 42);
			this.SolveButton.TabIndex = 7;
			this.SolveButton.Text = "Solve!";
			this.SolveButton.UseVisualStyleBackColor = true;
			this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
			// 
			// ClearButton
			// 
			this.ClearButton.Location = new System.Drawing.Point(504, 242);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(76, 42);
			this.ClearButton.TabIndex = 8;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 335);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 15);
			this.label10.TabIndex = 3;
			this.label10.Text = "Button:";
			// 
			// DragonButtonSelector
			// 
			this.DragonButtonSelector.Location = new System.Drawing.Point(64, 333);
			this.DragonButtonSelector.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.DragonButtonSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DragonButtonSelector.Name = "DragonButtonSelector";
			this.DragonButtonSelector.Size = new System.Drawing.Size(120, 23);
			this.DragonButtonSelector.TabIndex = 2;
			this.DragonButtonSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// TestDragonBtn
			// 
			this.TestDragonBtn.Location = new System.Drawing.Point(12, 375);
			this.TestDragonBtn.Name = "TestDragonBtn";
			this.TestDragonBtn.Size = new System.Drawing.Size(80, 32);
			this.TestDragonBtn.TabIndex = 4;
			this.TestDragonBtn.Text = "Test";
			this.TestDragonBtn.UseVisualStyleBackColor = true;
			this.TestDragonBtn.Click += new System.EventHandler(this.TestDragonBtn_Click);
			// 
			// LoadTextBox
			// 
			this.LoadTextBox.Location = new System.Drawing.Point(272, 295);
			this.LoadTextBox.Name = "LoadTextBox";
			this.LoadTextBox.Size = new System.Drawing.Size(417, 23);
			this.LoadTextBox.TabIndex = 0;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(227, 14);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66, 15);
			this.label11.TabIndex = 9;
			this.label11.Text = "Suit Stacks:";
			// 
			// SuitStack0
			// 
			this.SuitStack0.FormattingEnabled = true;
			this.SuitStack0.Items.AddRange(new object[] {
            "Empty",
            "Red",
            "Green",
            "Black"});
			this.SuitStack0.Location = new System.Drawing.Point(227, 35);
			this.SuitStack0.Name = "SuitStack0";
			this.SuitStack0.Size = new System.Drawing.Size(93, 23);
			this.SuitStack0.TabIndex = 10;
			// 
			// SuitStack1
			// 
			this.SuitStack1.FormattingEnabled = true;
			this.SuitStack1.Items.AddRange(new object[] {
            "Empty",
            "Red",
            "Green",
            "Black"});
			this.SuitStack1.Location = new System.Drawing.Point(326, 35);
			this.SuitStack1.Name = "SuitStack1";
			this.SuitStack1.Size = new System.Drawing.Size(93, 23);
			this.SuitStack1.TabIndex = 10;
			// 
			// SuitStack2
			// 
			this.SuitStack2.FormattingEnabled = true;
			this.SuitStack2.Items.AddRange(new object[] {
            "Empty",
            "Red",
            "Green",
            "Black"});
			this.SuitStack2.Location = new System.Drawing.Point(425, 35);
			this.SuitStack2.Name = "SuitStack2";
			this.SuitStack2.Size = new System.Drawing.Size(93, 23);
			this.SuitStack2.TabIndex = 10;
			// 
			// DebugBox
			// 
			this.DebugBox.AutoSize = true;
			this.DebugBox.Location = new System.Drawing.Point(390, 337);
			this.DebugBox.Name = "DebugBox";
			this.DebugBox.Size = new System.Drawing.Size(61, 19);
			this.DebugBox.TabIndex = 11;
			this.DebugBox.Text = "Debug";
			this.DebugBox.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1039, 461);
			this.Controls.Add(this.DebugBox);
			this.Controls.Add(this.SuitStack2);
			this.Controls.Add(this.SuitStack1);
			this.Controls.Add(this.SuitStack0);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.LoadTextBox);
			this.Controls.Add(this.TestDragonBtn);
			this.Controls.Add(this.DragonButtonSelector);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.ClearButton);
			this.Controls.Add(this.SolveButton);
			this.Controls.Add(this.TestBtn);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.CardSelector);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.TraySelector);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Card2YAdjustment);
			this.Controls.Add(this.Tray8XAdjustment);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Card1YAdjustment);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Tray1XAdjustment);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form 1";
			((System.ComponentModel.ISupportInitialize)(this.Tray1XAdjustment)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Card1YAdjustment)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Tray8XAdjustment)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Card2YAdjustment)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TraySelector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CardSelector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DragonButtonSelector)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown Tray1XAdjustment;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown Card1YAdjustment;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown Tray8XAdjustment;
		private System.Windows.Forms.NumericUpDown Card2YAdjustment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown TraySelector;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown CardSelector;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button TestBtn;
		private System.Windows.Forms.Button SolveButton;
		private System.Windows.Forms.Button ClearButton;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown DragonButtonSelector;
		private System.Windows.Forms.Button TestDragonBtn;
		private System.Windows.Forms.TextBox LoadTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox SuitStack0;
		private System.Windows.Forms.ComboBox SuitStack1;
		private System.Windows.Forms.ComboBox SuitStack2;
		private System.Windows.Forms.CheckBox DebugBox;
	}
}

