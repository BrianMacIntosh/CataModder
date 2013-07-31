namespace CataclysmModder
{
    partial class ToolValues
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.turnsPerNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.useChargesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.spawnChargesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxChargesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.revertsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ammoComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.turnsPerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useChargesNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnChargesNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxChargesNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.turnsPerNumeric);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.useChargesNumeric);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.spawnChargesNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maxChargesNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.revertsTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ammoComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tool Properties";
            // 
            // turnsPerNumeric
            // 
            this.turnsPerNumeric.Location = new System.Drawing.Point(450, 42);
            this.turnsPerNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.turnsPerNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.turnsPerNumeric.Name = "turnsPerNumeric";
            this.turnsPerNumeric.Size = new System.Drawing.Size(57, 20);
            this.turnsPerNumeric.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Turns Per Charge:";
            // 
            // useChargesNumeric
            // 
            this.useChargesNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.useChargesNumeric.Location = new System.Drawing.Point(450, 16);
            this.useChargesNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.useChargesNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.useChargesNumeric.Name = "useChargesNumeric";
            this.useChargesNumeric.Size = new System.Drawing.Size(57, 20);
            this.useChargesNumeric.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(354, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Charges Per Use:";
            // 
            // spawnChargesNumeric
            // 
            this.spawnChargesNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spawnChargesNumeric.Location = new System.Drawing.Point(278, 42);
            this.spawnChargesNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spawnChargesNumeric.Name = "spawnChargesNumeric";
            this.spawnChargesNumeric.Size = new System.Drawing.Size(57, 20);
            this.spawnChargesNumeric.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Spawn Charges:";
            // 
            // maxChargesNumeric
            // 
            this.maxChargesNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxChargesNumeric.Location = new System.Drawing.Point(278, 16);
            this.maxChargesNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.maxChargesNumeric.Name = "maxChargesNumeric";
            this.maxChargesNumeric.Size = new System.Drawing.Size(57, 20);
            this.maxChargesNumeric.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Max Charges:";
            // 
            // revertsTextBox
            // 
            this.revertsTextBox.Location = new System.Drawing.Point(79, 44);
            this.revertsTextBox.Name = "revertsTextBox";
            this.revertsTextBox.Size = new System.Drawing.Size(90, 20);
            this.revertsTextBox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Reverts To:";
            // 
            // ammoComboBox
            // 
            this.ammoComboBox.FormattingEnabled = true;
            this.ammoComboBox.Items.AddRange(new object[] {
            "NULL",
            "nail",
            "BB",
            "bolt",
            "arrow",
            "pebble",
            "shot",
            "22",
            "9mm",
            "762x25",
            "38",
            "40",
            "44",
            "45",
            "57",
            "46",
            "762",
            "223",
            "3006",
            "308",
            "40mm",
            "66mm",
            "gasoline",
            "THREAD",
            "battery",
            "plutonium",
            "muscle",
            "fusion",
            "12mm",
            "plasma",
            "water"});
            this.ammoComboBox.Location = new System.Drawing.Point(78, 16);
            this.ammoComboBox.Name = "ammoComboBox";
            this.ammoComboBox.Size = new System.Drawing.Size(91, 21);
            this.ammoComboBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Ammo Type:";
            // 
            // ToolValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ToolValues";
            this.Size = new System.Drawing.Size(626, 78);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.turnsPerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useChargesNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnChargesNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxChargesNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ammoComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxChargesNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox revertsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown turnsPerNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown useChargesNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown spawnChargesNumeric;
        private System.Windows.Forms.Label label4;
    }
}
