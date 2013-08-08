namespace CataclysmModder
{
    partial class Options
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.preserveUnchangedCheck = new System.Windows.Forms.CheckBox();
            this.indentSpacesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.indentTabsCheck = new System.Windows.Forms.CheckBox();
            this.dontFormatJsonCheck = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indentSpacesNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.preserveUnchangedCheck);
            this.groupBox1.Controls.Add(this.indentSpacesNumeric);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.indentTabsCheck);
            this.groupBox1.Controls.Add(this.dontFormatJsonCheck);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Options";
            // 
            // preserveUnchangedCheck
            // 
            this.preserveUnchangedCheck.AutoSize = true;
            this.preserveUnchangedCheck.Location = new System.Drawing.Point(7, 94);
            this.preserveUnchangedCheck.Name = "preserveUnchangedCheck";
            this.preserveUnchangedCheck.Size = new System.Drawing.Size(149, 17);
            this.preserveUnchangedCheck.TabIndex = 4;
            this.preserveUnchangedCheck.Text = "Preserve unchanged data";
            this.preserveUnchangedCheck.UseVisualStyleBackColor = true;
            // 
            // indentSpacesNumeric
            // 
            this.indentSpacesNumeric.Location = new System.Drawing.Point(85, 68);
            this.indentSpacesNumeric.Name = "indentSpacesNumeric";
            this.indentSpacesNumeric.Size = new System.Drawing.Size(43, 20);
            this.indentSpacesNumeric.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Indent Spaces:";
            // 
            // indentTabsCheck
            // 
            this.indentTabsCheck.AutoSize = true;
            this.indentTabsCheck.Location = new System.Drawing.Point(7, 44);
            this.indentTabsCheck.Name = "indentTabsCheck";
            this.indentTabsCheck.Size = new System.Drawing.Size(105, 17);
            this.indentTabsCheck.TabIndex = 1;
            this.indentTabsCheck.Text = "Indent with Tabs";
            this.indentTabsCheck.UseVisualStyleBackColor = true;
            this.indentTabsCheck.CheckedChanged += new System.EventHandler(this.indentTabsCheck_CheckedChanged);
            // 
            // dontFormatJsonCheck
            // 
            this.dontFormatJsonCheck.AutoSize = true;
            this.dontFormatJsonCheck.Location = new System.Drawing.Point(7, 20);
            this.dontFormatJsonCheck.Name = "dontFormatJsonCheck";
            this.dontFormatJsonCheck.Size = new System.Drawing.Size(121, 17);
            this.dontFormatJsonCheck.TabIndex = 0;
            this.dontFormatJsonCheck.Text = "Do not format JSON";
            this.dontFormatJsonCheck.UseVisualStyleBackColor = true;
            this.dontFormatJsonCheck.CheckedChanged += new System.EventHandler(this.dontFormatJsonCheck_CheckedChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(94, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(13, 227);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // Options
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(184, 262);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indentSpacesNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown indentSpacesNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox indentTabsCheck;
        private System.Windows.Forms.CheckBox dontFormatJsonCheck;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.CheckBox preserveUnchangedCheck;
    }
}