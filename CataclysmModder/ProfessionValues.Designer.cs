namespace CataclysmModder
{
    partial class ProfessionValues
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.addictionStrengthNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.addictionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.addictionsListBox = new System.Windows.Forms.ListBox();
            this.deleteAddiction = new System.Windows.Forms.Button();
            this.newAddiction = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.skillLevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.skillComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.skillsListBox = new System.Windows.Forms.ListBox();
            this.deleteSkillButton = new System.Windows.Forms.Button();
            this.newSkillButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.itemIdTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.itemListBox = new System.Windows.Forms.ListBox();
            this.deleteItemButton = new System.Windows.Forms.Button();
            this.newItemButton = new System.Windows.Forms.Button();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pointsNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addictionStrengthNumeric)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillLevelNumeric)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointsNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.descTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pointsNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nameTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.idTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 301);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profession";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.addictionStrengthNumeric);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.addictionTypeComboBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.addictionsListBox);
            this.groupBox4.Controls.Add(this.deleteAddiction);
            this.groupBox4.Controls.Add(this.newAddiction);
            this.groupBox4.Location = new System.Drawing.Point(364, 99);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(171, 193);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Addictions";
            // 
            // addictionStrengthNumeric
            // 
            this.addictionStrengthNumeric.Location = new System.Drawing.Point(53, 165);
            this.addictionStrengthNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.addictionStrengthNumeric.Name = "addictionStrengthNumeric";
            this.addictionStrengthNumeric.Size = new System.Drawing.Size(68, 20);
            this.addictionStrengthNumeric.TabIndex = 6;
            this.addictionStrengthNumeric.ValueChanged += new System.EventHandler(this.addictionStrengthNumeric_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Str:";
            // 
            // addictionTypeComboBox
            // 
            this.addictionTypeComboBox.FormattingEnabled = true;
            this.addictionTypeComboBox.Location = new System.Drawing.Point(53, 138);
            this.addictionTypeComboBox.Name = "addictionTypeComboBox";
            this.addictionTypeComboBox.Size = new System.Drawing.Size(111, 21);
            this.addictionTypeComboBox.TabIndex = 4;
            this.addictionTypeComboBox.TextChanged += new System.EventHandler(this.addictionTypeComboBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Type:";
            // 
            // addictionsListBox
            // 
            this.addictionsListBox.FormattingEnabled = true;
            this.addictionsListBox.Location = new System.Drawing.Point(7, 50);
            this.addictionsListBox.Name = "addictionsListBox";
            this.addictionsListBox.Size = new System.Drawing.Size(157, 82);
            this.addictionsListBox.TabIndex = 2;
            this.addictionsListBox.SelectedIndexChanged += new System.EventHandler(this.addictionsListBox_SelectedIndexChanged);
            // 
            // deleteAddiction
            // 
            this.deleteAddiction.Location = new System.Drawing.Point(89, 20);
            this.deleteAddiction.Name = "deleteAddiction";
            this.deleteAddiction.Size = new System.Drawing.Size(75, 23);
            this.deleteAddiction.TabIndex = 1;
            this.deleteAddiction.Text = "Delete";
            this.deleteAddiction.UseVisualStyleBackColor = true;
            this.deleteAddiction.Click += new System.EventHandler(this.button1_Click);
            // 
            // newAddiction
            // 
            this.newAddiction.Location = new System.Drawing.Point(7, 20);
            this.newAddiction.Name = "newAddiction";
            this.newAddiction.Size = new System.Drawing.Size(75, 23);
            this.newAddiction.TabIndex = 0;
            this.newAddiction.Text = "New";
            this.newAddiction.UseVisualStyleBackColor = true;
            this.newAddiction.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.skillLevelNumeric);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.skillComboBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.skillsListBox);
            this.groupBox3.Controls.Add(this.deleteSkillButton);
            this.groupBox3.Controls.Add(this.newSkillButton);
            this.groupBox3.Location = new System.Drawing.Point(187, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(171, 193);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Skills";
            // 
            // skillLevelNumeric
            // 
            this.skillLevelNumeric.Location = new System.Drawing.Point(53, 165);
            this.skillLevelNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.skillLevelNumeric.Name = "skillLevelNumeric";
            this.skillLevelNumeric.Size = new System.Drawing.Size(68, 20);
            this.skillLevelNumeric.TabIndex = 6;
            this.skillLevelNumeric.ValueChanged += new System.EventHandler(this.skillLevelNumeric_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Level:";
            // 
            // skillComboBox
            // 
            this.skillComboBox.FormattingEnabled = true;
            this.skillComboBox.Location = new System.Drawing.Point(53, 138);
            this.skillComboBox.Name = "skillComboBox";
            this.skillComboBox.Size = new System.Drawing.Size(111, 21);
            this.skillComboBox.TabIndex = 4;
            this.skillComboBox.TextChanged += new System.EventHandler(this.skillComboBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Skill:";
            // 
            // skillsListBox
            // 
            this.skillsListBox.FormattingEnabled = true;
            this.skillsListBox.Location = new System.Drawing.Point(7, 50);
            this.skillsListBox.Name = "skillsListBox";
            this.skillsListBox.Size = new System.Drawing.Size(157, 82);
            this.skillsListBox.TabIndex = 2;
            this.skillsListBox.SelectedIndexChanged += new System.EventHandler(this.skillsListBox_SelectedIndexChanged);
            // 
            // deleteSkillButton
            // 
            this.deleteSkillButton.Location = new System.Drawing.Point(89, 20);
            this.deleteSkillButton.Name = "deleteSkillButton";
            this.deleteSkillButton.Size = new System.Drawing.Size(75, 23);
            this.deleteSkillButton.TabIndex = 1;
            this.deleteSkillButton.Text = "Delete";
            this.deleteSkillButton.UseVisualStyleBackColor = true;
            this.deleteSkillButton.Click += new System.EventHandler(this.deleteSkillButton_Click);
            // 
            // newSkillButton
            // 
            this.newSkillButton.Location = new System.Drawing.Point(7, 20);
            this.newSkillButton.Name = "newSkillButton";
            this.newSkillButton.Size = new System.Drawing.Size(75, 23);
            this.newSkillButton.TabIndex = 0;
            this.newSkillButton.Text = "New";
            this.newSkillButton.UseVisualStyleBackColor = true;
            this.newSkillButton.Click += new System.EventHandler(this.newSkillButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.itemIdTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.itemListBox);
            this.groupBox2.Controls.Add(this.deleteItemButton);
            this.groupBox2.Controls.Add(this.newItemButton);
            this.groupBox2.Location = new System.Drawing.Point(10, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 193);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Items";
            // 
            // itemIdTextBox
            // 
            this.itemIdTextBox.Location = new System.Drawing.Point(31, 138);
            this.itemIdTextBox.Name = "itemIdTextBox";
            this.itemIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.itemIdTextBox.TabIndex = 4;
            this.itemIdTextBox.TextChanged += new System.EventHandler(this.itemIdTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Id:";
            // 
            // itemListBox
            // 
            this.itemListBox.FormattingEnabled = true;
            this.itemListBox.Location = new System.Drawing.Point(7, 50);
            this.itemListBox.Name = "itemListBox";
            this.itemListBox.Size = new System.Drawing.Size(157, 82);
            this.itemListBox.TabIndex = 2;
            this.itemListBox.SelectedIndexChanged += new System.EventHandler(this.itemListBox_SelectedIndexChanged);
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.Location = new System.Drawing.Point(89, 20);
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(75, 23);
            this.deleteItemButton.TabIndex = 1;
            this.deleteItemButton.Text = "Delete";
            this.deleteItemButton.UseVisualStyleBackColor = true;
            this.deleteItemButton.Click += new System.EventHandler(this.deleteItemButton_Click);
            // 
            // newItemButton
            // 
            this.newItemButton.Location = new System.Drawing.Point(7, 20);
            this.newItemButton.Name = "newItemButton";
            this.newItemButton.Size = new System.Drawing.Size(75, 23);
            this.newItemButton.TabIndex = 0;
            this.newItemButton.Text = "New";
            this.newItemButton.UseVisualStyleBackColor = true;
            this.newItemButton.Click += new System.EventHandler(this.newItemButton_Click);
            // 
            // descTextBox
            // 
            this.descTextBox.AcceptsReturn = true;
            this.descTextBox.Location = new System.Drawing.Point(173, 36);
            this.descTextBox.Multiline = true;
            this.descTextBox.Name = "descTextBox";
            this.descTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descTextBox.Size = new System.Drawing.Size(440, 57);
            this.descTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description:";
            // 
            // pointsNumeric
            // 
            this.pointsNumeric.Location = new System.Drawing.Point(51, 73);
            this.pointsNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.pointsNumeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.pointsNumeric.Name = "pointsNumeric";
            this.pointsNumeric.Size = new System.Drawing.Size(76, 20);
            this.pointsNumeric.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Points:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(51, 46);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(51, 20);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id:";
            // 
            // ProfessionValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ProfessionValues";
            this.Size = new System.Drawing.Size(626, 307);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addictionStrengthNumeric)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillLevelNumeric)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointsNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown pointsNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown addictionStrengthNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox addictionTypeComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox addictionsListBox;
        private System.Windows.Forms.Button deleteAddiction;
        private System.Windows.Forms.Button newAddiction;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown skillLevelNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox skillComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox skillsListBox;
        private System.Windows.Forms.Button deleteSkillButton;
        private System.Windows.Forms.Button newSkillButton;
        private System.Windows.Forms.TextBox itemIdTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox itemListBox;
        private System.Windows.Forms.Button deleteItemButton;
        private System.Windows.Forms.Button newItemButton;
    }
}
