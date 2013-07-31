namespace CataclysmModder
{
    partial class BookValues
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
            this.skillComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.funNumeric = new System.Windows.Forms.NumericUpDown();
            this.timeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.reqlevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxlevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.intNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.funNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reqlevelNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxlevelNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intNumeric);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.reqlevelNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maxlevelNumeric);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.timeNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.funNumeric);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.skillComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Book Properties";
            // 
            // skillComboBox
            // 
            this.skillComboBox.FormattingEnabled = true;
            this.skillComboBox.Location = new System.Drawing.Point(43, 20);
            this.skillComboBox.Name = "skillComboBox";
            this.skillComboBox.Size = new System.Drawing.Size(103, 21);
            this.skillComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skill:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fun:";
            // 
            // funNumeric
            // 
            this.funNumeric.Location = new System.Drawing.Point(194, 20);
            this.funNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.funNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.funNumeric.Name = "funNumeric";
            this.funNumeric.Size = new System.Drawing.Size(66, 20);
            this.funNumeric.TabIndex = 3;
            // 
            // timeNumeric
            // 
            this.timeNumeric.Location = new System.Drawing.Point(194, 46);
            this.timeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.timeNumeric.Name = "timeNumeric";
            this.timeNumeric.Size = new System.Drawing.Size(66, 20);
            this.timeNumeric.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Time:";
            // 
            // reqlevelNumeric
            // 
            this.reqlevelNumeric.Location = new System.Drawing.Point(331, 46);
            this.reqlevelNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.reqlevelNumeric.Name = "reqlevelNumeric";
            this.reqlevelNumeric.Size = new System.Drawing.Size(66, 20);
            this.reqlevelNumeric.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Req. Level:";
            // 
            // maxlevelNumeric
            // 
            this.maxlevelNumeric.Location = new System.Drawing.Point(331, 20);
            this.maxlevelNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.maxlevelNumeric.Name = "maxlevelNumeric";
            this.maxlevelNumeric.Size = new System.Drawing.Size(66, 20);
            this.maxlevelNumeric.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Max Level:";
            // 
            // intNumeric
            // 
            this.intNumeric.Location = new System.Drawing.Point(72, 46);
            this.intNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.intNumeric.Name = "intNumeric";
            this.intNumeric.Size = new System.Drawing.Size(66, 20);
            this.intNumeric.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Req. Int:";
            // 
            // BookValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "BookValues";
            this.Size = new System.Drawing.Size(626, 79);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.funNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reqlevelNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxlevelNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox skillComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown funNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown intNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown reqlevelNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxlevelNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown timeNumeric;
        private System.Windows.Forms.Label label3;
    }
}
