namespace CataclysmModder
{
    partial class RecipeControl
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
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.suffixTextBox = new System.Windows.Forms.TextBox();
            this.diffNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.timeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.autolearnCheckBox = new System.Windows.Forms.CheckBox();
            this.reversibleCheckBox = new System.Windows.Forms.CheckBox();
            this.skill1ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.skill2ComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.disLearnNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diffNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disLearnNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.disLearnNumeric);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.skill2ComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.skill1ComboBox);
            this.groupBox1.Controls.Add(this.reversibleCheckBox);
            this.groupBox1.Controls.Add(this.autolearnCheckBox);
            this.groupBox1.Controls.Add(this.categoryComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.timeNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.diffNumeric);
            this.groupBox1.Controls.Add(this.suffixTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.resultTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipe Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Result:";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(82, 20);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(121, 20);
            this.resultTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Suffix:";
            // 
            // suffixTextBox
            // 
            this.suffixTextBox.Location = new System.Drawing.Point(82, 46);
            this.suffixTextBox.Name = "suffixTextBox";
            this.suffixTextBox.Size = new System.Drawing.Size(121, 20);
            this.suffixTextBox.TabIndex = 3;
            // 
            // diffNumeric
            // 
            this.diffNumeric.Location = new System.Drawing.Point(82, 126);
            this.diffNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.diffNumeric.Name = "diffNumeric";
            this.diffNumeric.Size = new System.Drawing.Size(59, 20);
            this.diffNumeric.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Difficulty:";
            // 
            // timeNumeric
            // 
            this.timeNumeric.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeNumeric.Location = new System.Drawing.Point(82, 153);
            this.timeNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.timeNumeric.Name = "timeNumeric";
            this.timeNumeric.Size = new System.Drawing.Size(59, 20);
            this.timeNumeric.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Category:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(82, 180);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(121, 21);
            this.categoryComboBox.TabIndex = 9;
            // 
            // autolearnCheckBox
            // 
            this.autolearnCheckBox.AutoSize = true;
            this.autolearnCheckBox.Location = new System.Drawing.Point(23, 207);
            this.autolearnCheckBox.Name = "autolearnCheckBox";
            this.autolearnCheckBox.Size = new System.Drawing.Size(74, 17);
            this.autolearnCheckBox.TabIndex = 10;
            this.autolearnCheckBox.Text = "Auto-learn";
            this.autolearnCheckBox.UseVisualStyleBackColor = true;
            // 
            // reversibleCheckBox
            // 
            this.reversibleCheckBox.AutoSize = true;
            this.reversibleCheckBox.Location = new System.Drawing.Point(109, 207);
            this.reversibleCheckBox.Name = "reversibleCheckBox";
            this.reversibleCheckBox.Size = new System.Drawing.Size(76, 17);
            this.reversibleCheckBox.TabIndex = 11;
            this.reversibleCheckBox.Text = "Reversible";
            this.reversibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // skill1ComboBox
            // 
            this.skill1ComboBox.FormattingEnabled = true;
            this.skill1ComboBox.Location = new System.Drawing.Point(82, 72);
            this.skill1ComboBox.Name = "skill1ComboBox";
            this.skill1ComboBox.Size = new System.Drawing.Size(121, 21);
            this.skill1ComboBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Primary Skill:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Secndry Skill:";
            // 
            // skill2ComboBox
            // 
            this.skill2ComboBox.FormattingEnabled = true;
            this.skill2ComboBox.Location = new System.Drawing.Point(82, 99);
            this.skill2ComboBox.Name = "skill2ComboBox";
            this.skill2ComboBox.Size = new System.Drawing.Size(121, 21);
            this.skill2ComboBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Learn by Disassembly:";
            // 
            // disLearnNumeric
            // 
            this.disLearnNumeric.Location = new System.Drawing.Point(132, 239);
            this.disLearnNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.disLearnNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.disLearnNumeric.Name = "disLearnNumeric";
            this.disLearnNumeric.Size = new System.Drawing.Size(71, 20);
            this.disLearnNumeric.TabIndex = 17;
            // 
            // RecipeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RecipeControl";
            this.Size = new System.Drawing.Size(626, 280);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diffNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disLearnNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown timeNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown diffNumeric;
        private System.Windows.Forms.TextBox suffixTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown disLearnNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox skill2ComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox skill1ComboBox;
        private System.Windows.Forms.CheckBox reversibleCheckBox;
        private System.Windows.Forms.CheckBox autolearnCheckBox;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label label5;
    }
}
