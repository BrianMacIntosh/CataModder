namespace CataclysmModder
{
    partial class AmmoValues
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
            this.label1 = new System.Windows.Forms.Label();
            this.ammoComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.effectsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.countNumeric = new System.Windows.Forms.NumericUpDown();
            this.dispersionNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.recoilNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.rangeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.pierceNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.damageNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoilNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pierceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rangeNumeric);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.pierceNumeric);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.damageNumeric);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.recoilNumeric);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dispersionNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.countNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.effectsCheckedListBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ammoComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ammo Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ammo Type:";
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
            this.ammoComboBox.Location = new System.Drawing.Point(79, 20);
            this.ammoComboBox.Name = "ammoComboBox";
            this.ammoComboBox.Size = new System.Drawing.Size(91, 21);
            this.ammoComboBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Effects:";
            // 
            // effectsCheckedListBox
            // 
            this.effectsCheckedListBox.FormattingEnabled = true;
            this.effectsCheckedListBox.Items.AddRange(new object[] {
            "EXPLOSIVE_BIG",
            "EXPLOSIVE",
            "FLAME",
            "INCENDIARY",
            "IGNITE",
            "BOUNCE",
            "FRAG",
            "NAPALM",
            "ACIDBOMB",
            "TEARGAS",
            "SMOKE",
            "FLASHBANG",
            "LIGHTNING"});
            this.effectsCheckedListBox.Location = new System.Drawing.Point(352, 32);
            this.effectsCheckedListBox.Name = "effectsCheckedListBox";
            this.effectsCheckedListBox.Size = new System.Drawing.Size(157, 109);
            this.effectsCheckedListBox.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Initial Count:";
            // 
            // countNumeric
            // 
            this.countNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.countNumeric.Location = new System.Drawing.Point(82, 49);
            this.countNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.countNumeric.Name = "countNumeric";
            this.countNumeric.Size = new System.Drawing.Size(75, 20);
            this.countNumeric.TabIndex = 20;
            // 
            // dispersionNumeric
            // 
            this.dispersionNumeric.Location = new System.Drawing.Point(82, 75);
            this.dispersionNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.dispersionNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.dispersionNumeric.Name = "dispersionNumeric";
            this.dispersionNumeric.Size = new System.Drawing.Size(75, 20);
            this.dispersionNumeric.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Dispersion:";
            // 
            // recoilNumeric
            // 
            this.recoilNumeric.Location = new System.Drawing.Point(82, 101);
            this.recoilNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.recoilNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.recoilNumeric.Name = "recoilNumeric";
            this.recoilNumeric.Size = new System.Drawing.Size(75, 20);
            this.recoilNumeric.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Recoil:";
            // 
            // rangeNumeric
            // 
            this.rangeNumeric.Location = new System.Drawing.Point(256, 72);
            this.rangeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.rangeNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.rangeNumeric.Name = "rangeNumeric";
            this.rangeNumeric.Size = new System.Drawing.Size(75, 20);
            this.rangeNumeric.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Range:";
            // 
            // pierceNumeric
            // 
            this.pierceNumeric.Location = new System.Drawing.Point(256, 46);
            this.pierceNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.pierceNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.pierceNumeric.Name = "pierceNumeric";
            this.pierceNumeric.Size = new System.Drawing.Size(75, 20);
            this.pierceNumeric.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Armor Pierce:";
            // 
            // damageNumeric
            // 
            this.damageNumeric.Location = new System.Drawing.Point(256, 20);
            this.damageNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.damageNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.damageNumeric.Name = "damageNumeric";
            this.damageNumeric.Size = new System.Drawing.Size(75, 20);
            this.damageNumeric.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Damage:";
            // 
            // AmmoValues
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "AmmoValues";
            this.Size = new System.Drawing.Size(626, 150);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoilNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pierceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown countNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox effectsCheckedListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ammoComboBox;
        private System.Windows.Forms.NumericUpDown rangeNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown pierceNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown damageNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown recoilNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown dispersionNumeric;
        private System.Windows.Forms.Label label4;
    }
}
