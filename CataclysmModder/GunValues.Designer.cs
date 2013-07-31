namespace CataclysmModder
{
    partial class GunValues
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
            this.skillComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.damageNumeric = new System.Windows.Forms.NumericUpDown();
            this.rangeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dispersionNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.recoilNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.reloadTimeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.clipsizeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.burstNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.durabilityNumeric = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.damageNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoilNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadTimeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipsizeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.burstNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.durabilityNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reloadTimeNumeric);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.clipsizeNumeric);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.burstNumeric);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.durabilityNumeric);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.recoilNumeric);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dispersionNumeric);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rangeNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.damageNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.skillComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ammoComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gun Properties:";
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
            this.ammoComboBox.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Skill Used:";
            // 
            // skillComboBox
            // 
            this.skillComboBox.FormattingEnabled = true;
            this.skillComboBox.Location = new System.Drawing.Point(79, 50);
            this.skillComboBox.Name = "skillComboBox";
            this.skillComboBox.Size = new System.Drawing.Size(91, 21);
            this.skillComboBox.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Damage:";
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
            this.damageNumeric.Size = new System.Drawing.Size(74, 20);
            this.damageNumeric.TabIndex = 19;
            // 
            // rangeNumeric
            // 
            this.rangeNumeric.Location = new System.Drawing.Point(256, 46);
            this.rangeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.rangeNumeric.Name = "rangeNumeric";
            this.rangeNumeric.Size = new System.Drawing.Size(74, 20);
            this.rangeNumeric.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Range:";
            // 
            // dispersionNumeric
            // 
            this.dispersionNumeric.Location = new System.Drawing.Point(256, 72);
            this.dispersionNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.dispersionNumeric.Name = "dispersionNumeric";
            this.dispersionNumeric.Size = new System.Drawing.Size(74, 20);
            this.dispersionNumeric.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Dispersion:";
            // 
            // recoilNumeric
            // 
            this.recoilNumeric.Location = new System.Drawing.Point(256, 98);
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
            this.recoilNumeric.Size = new System.Drawing.Size(74, 20);
            this.recoilNumeric.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Recoil:";
            // 
            // reloadTimeNumeric
            // 
            this.reloadTimeNumeric.Location = new System.Drawing.Point(430, 99);
            this.reloadTimeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.reloadTimeNumeric.Name = "reloadTimeNumeric";
            this.reloadTimeNumeric.Size = new System.Drawing.Size(74, 20);
            this.reloadTimeNumeric.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(354, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Reload Time:";
            // 
            // clipsizeNumeric
            // 
            this.clipsizeNumeric.Location = new System.Drawing.Point(430, 73);
            this.clipsizeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.clipsizeNumeric.Name = "clipsizeNumeric";
            this.clipsizeNumeric.Size = new System.Drawing.Size(74, 20);
            this.clipsizeNumeric.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(354, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Clip Size:";
            // 
            // burstNumeric
            // 
            this.burstNumeric.Location = new System.Drawing.Point(430, 47);
            this.burstNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.burstNumeric.Name = "burstNumeric";
            this.burstNumeric.Size = new System.Drawing.Size(74, 20);
            this.burstNumeric.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(354, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Burst:";
            // 
            // durabilityNumeric
            // 
            this.durabilityNumeric.Location = new System.Drawing.Point(430, 21);
            this.durabilityNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.durabilityNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.durabilityNumeric.Name = "durabilityNumeric";
            this.durabilityNumeric.Size = new System.Drawing.Size(74, 20);
            this.durabilityNumeric.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(354, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Durability:";
            // 
            // GunValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "GunValues";
            this.Size = new System.Drawing.Size(626, 136);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.damageNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recoilNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadTimeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipsizeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.burstNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.durabilityNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox skillComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ammoComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown reloadTimeNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown clipsizeNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown burstNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown durabilityNumeric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown recoilNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown dispersionNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown rangeNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown damageNumeric;
    }
}
