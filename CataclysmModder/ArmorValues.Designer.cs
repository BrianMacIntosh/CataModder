namespace CataclysmModder
{
    partial class ArmorValues
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
            this.coversCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.encumberanceNumeric = new System.Windows.Forms.NumericUpDown();
            this.coverageNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.thicknessNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.storageNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.warmthNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.enviProtectNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.powerArmorCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.encumberanceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coverageNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thicknessNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warmthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enviProtectNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.powerArmorCheckBox);
            this.groupBox1.Controls.Add(this.storageNumeric);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.warmthNumeric);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.enviProtectNumeric);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.thicknessNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.coverageNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.encumberanceNumeric);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.coversCheckedListBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Armor Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Covers:";
            // 
            // coversCheckedListBox
            // 
            this.coversCheckedListBox.FormattingEnabled = true;
            this.coversCheckedListBox.Items.AddRange(new object[] {
            "TORSO",
            "HEAD",
            "EYES",
            "MOUTH",
            "ARMS",
            "HANDS",
            "LEGS",
            "FEET"});
            this.coversCheckedListBox.Location = new System.Drawing.Point(10, 37);
            this.coversCheckedListBox.Name = "coversCheckedListBox";
            this.coversCheckedListBox.Size = new System.Drawing.Size(120, 94);
            this.coversCheckedListBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Encumberance:";
            // 
            // encumberanceNumeric
            // 
            this.encumberanceNumeric.Location = new System.Drawing.Point(236, 20);
            this.encumberanceNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.encumberanceNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.encumberanceNumeric.Name = "encumberanceNumeric";
            this.encumberanceNumeric.Size = new System.Drawing.Size(63, 20);
            this.encumberanceNumeric.TabIndex = 3;
            // 
            // coverageNumeric
            // 
            this.coverageNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.coverageNumeric.Location = new System.Drawing.Point(236, 46);
            this.coverageNumeric.Name = "coverageNumeric";
            this.coverageNumeric.Size = new System.Drawing.Size(63, 20);
            this.coverageNumeric.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Coverage:";
            // 
            // thicknessNumeric
            // 
            this.thicknessNumeric.Location = new System.Drawing.Point(236, 72);
            this.thicknessNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.thicknessNumeric.Name = "thicknessNumeric";
            this.thicknessNumeric.Size = new System.Drawing.Size(63, 20);
            this.thicknessNumeric.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Thickness:";
            // 
            // storageNumeric
            // 
            this.storageNumeric.Location = new System.Drawing.Point(394, 72);
            this.storageNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.storageNumeric.Name = "storageNumeric";
            this.storageNumeric.Size = new System.Drawing.Size(63, 20);
            this.storageNumeric.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Storage:";
            // 
            // warmthNumeric
            // 
            this.warmthNumeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.warmthNumeric.Location = new System.Drawing.Point(394, 46);
            this.warmthNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.warmthNumeric.Name = "warmthNumeric";
            this.warmthNumeric.Size = new System.Drawing.Size(63, 20);
            this.warmthNumeric.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warmth:";
            // 
            // enviProtectNumeric
            // 
            this.enviProtectNumeric.Location = new System.Drawing.Point(394, 20);
            this.enviProtectNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.enviProtectNumeric.Name = "enviProtectNumeric";
            this.enviProtectNumeric.Size = new System.Drawing.Size(63, 20);
            this.enviProtectNumeric.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Envi. Protection:";
            // 
            // powerArmorCheckBox
            // 
            this.powerArmorCheckBox.AutoSize = true;
            this.powerArmorCheckBox.Location = new System.Drawing.Point(257, 113);
            this.powerArmorCheckBox.Name = "powerArmorCheckBox";
            this.powerArmorCheckBox.Size = new System.Drawing.Size(97, 17);
            this.powerArmorCheckBox.TabIndex = 14;
            this.powerArmorCheckBox.Text = "Is Power Armor";
            this.powerArmorCheckBox.UseVisualStyleBackColor = true;
            // 
            // ArmorValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ArmorValues";
            this.Size = new System.Drawing.Size(626, 150);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.encumberanceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coverageNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thicknessNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warmthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enviProtectNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown encumberanceNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox coversCheckedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox powerArmorCheckBox;
        private System.Windows.Forms.NumericUpDown storageNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown warmthNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown enviProtectNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown thicknessNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown coverageNumeric;
        private System.Windows.Forms.Label label3;
    }
}
