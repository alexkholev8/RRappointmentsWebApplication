namespace SetupSettingsClassLibray
{
    partial class SettingsWindow
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
            this.siteSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.virtualDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.loadConfiurationButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.wsDISHtextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wsPortTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wsIPtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.appointmentLimitComboBox = new System.Windows.Forms.ComboBox();
            this.appointmentLimitCheckBox = new System.Windows.Forms.CheckBox();
            this.enableTreatmentGroupsCheckBox = new System.Windows.Forms.CheckBox();
            this.FinishButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.wsUserIdTextBox = new System.Windows.Forms.TextBox();
            this.wsPasswordTextBox = new System.Windows.Forms.TextBox();
            this.siteSettingsGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // siteSettingsGroupBox
            // 
            this.siteSettingsGroupBox.Controls.Add(this.label2);
            this.siteSettingsGroupBox.Controls.Add(this.virtualDirectoryTextBox);
            this.siteSettingsGroupBox.Controls.Add(this.loadConfiurationButton);
            this.siteSettingsGroupBox.Controls.Add(this.groupBox2);
            this.siteSettingsGroupBox.Controls.Add(this.groupBox1);
            this.siteSettingsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.siteSettingsGroupBox.Location = new System.Drawing.Point(9, 12);
            this.siteSettingsGroupBox.Name = "siteSettingsGroupBox";
            this.siteSettingsGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.siteSettingsGroupBox.Size = new System.Drawing.Size(612, 342);
            this.siteSettingsGroupBox.TabIndex = 3;
            this.siteSettingsGroupBox.TabStop = false;
            this.siteSettingsGroupBox.Text = "הגדרות אתר";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(479, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "שם ספרייה וירטואלית:";
            // 
            // virtualDirectoryTextBox
            // 
            this.virtualDirectoryTextBox.Location = new System.Drawing.Point(284, 20);
            this.virtualDirectoryTextBox.Name = "virtualDirectoryTextBox";
            this.virtualDirectoryTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.virtualDirectoryTextBox.Size = new System.Drawing.Size(180, 21);
            this.virtualDirectoryTextBox.TabIndex = 3;
            this.virtualDirectoryTextBox.Text = "/RRsite/";
            // 
            // loadConfiurationButton
            // 
            this.loadConfiurationButton.Location = new System.Drawing.Point(195, 18);
            this.loadConfiurationButton.Name = "loadConfiurationButton";
            this.loadConfiurationButton.Size = new System.Drawing.Size(83, 23);
            this.loadConfiurationButton.TabIndex = 2;
            this.loadConfiurationButton.Text = "טען הגדרות";
            this.loadConfiurationButton.UseVisualStyleBackColor = true;
            this.loadConfiurationButton.Click += new System.EventHandler(this.loadConfiurationButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.wsPasswordTextBox);
            this.groupBox2.Controls.Add(this.wsUserIdTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.wsDISHtextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.wsPortTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.wsIPtextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox2.Location = new System.Drawing.Point(16, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 230);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "הגדרות WS";
            // 
            // wsDISHtextBox
            // 
            this.wsDISHtextBox.Location = new System.Drawing.Point(6, 193);
            this.wsDISHtextBox.Name = "wsDISHtextBox";
            this.wsDISHtextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wsDISHtextBox.Size = new System.Drawing.Size(322, 21);
            this.wsDISHtextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "כתובת שירות DISH:";
            // 
            // wsPortTextBox
            // 
            this.wsPortTextBox.Location = new System.Drawing.Point(16, 140);
            this.wsPortTextBox.Name = "wsPortTextBox";
            this.wsPortTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wsPortTextBox.Size = new System.Drawing.Size(65, 21);
            this.wsPortTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "פורט:";
            // 
            // wsIPtextBox
            // 
            this.wsIPtextBox.Location = new System.Drawing.Point(16, 107);
            this.wsIPtextBox.Name = "wsIPtextBox";
            this.wsIPtextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wsIPtextBox.Size = new System.Drawing.Size(255, 21);
            this.wsIPtextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "כתובת IP:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.appointmentLimitComboBox);
            this.groupBox1.Controls.Add(this.appointmentLimitCheckBox);
            this.groupBox1.Controls.Add(this.enableTreatmentGroupsCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(360, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "מאפייני אתר";
            // 
            // appointmentLimitComboBox
            // 
            this.appointmentLimitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appointmentLimitComboBox.FormattingEnabled = true;
            this.appointmentLimitComboBox.Location = new System.Drawing.Point(18, 52);
            this.appointmentLimitComboBox.Name = "appointmentLimitComboBox";
            this.appointmentLimitComboBox.Size = new System.Drawing.Size(46, 23);
            this.appointmentLimitComboBox.TabIndex = 2;
            // 
            // appointmentLimitCheckBox
            // 
            this.appointmentLimitCheckBox.AutoSize = true;
            this.appointmentLimitCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.appointmentLimitCheckBox.Location = new System.Drawing.Point(89, 56);
            this.appointmentLimitCheckBox.Name = "appointmentLimitCheckBox";
            this.appointmentLimitCheckBox.Size = new System.Drawing.Size(151, 17);
            this.appointmentLimitCheckBox.TabIndex = 1;
            this.appointmentLimitCheckBox.Text = "הגבל מספר זימוני תורים";
            this.appointmentLimitCheckBox.UseVisualStyleBackColor = true;
            this.appointmentLimitCheckBox.CheckedChanged += new System.EventHandler(this.appointmentLimitCheckBox_CheckedChanged);
            // 
            // enableTreatmentGroupsCheckBox
            // 
            this.enableTreatmentGroupsCheckBox.AutoSize = true;
            this.enableTreatmentGroupsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.enableTreatmentGroupsCheckBox.Location = new System.Drawing.Point(72, 29);
            this.enableTreatmentGroupsCheckBox.Name = "enableTreatmentGroupsCheckBox";
            this.enableTreatmentGroupsCheckBox.Size = new System.Drawing.Size(168, 17);
            this.enableTreatmentGroupsCheckBox.TabIndex = 0;
            this.enableTreatmentGroupsCheckBox.Text = "הפעל מיון ע\"פ סוגי בדיקות";
            this.enableTreatmentGroupsCheckBox.UseVisualStyleBackColor = true;
            // 
            // FinishButton
            // 
            this.FinishButton.Enabled = false;
            this.FinishButton.Location = new System.Drawing.Point(534, 360);
            this.FinishButton.Name = "FinishButton";
            this.FinishButton.Size = new System.Drawing.Size(75, 23);
            this.FinishButton.TabIndex = 4;
            this.FinishButton.Text = "סיום";
            this.FinishButton.UseVisualStyleBackColor = true;
            this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(25, 360);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "ביטול";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "שם משתמש:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(288, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "סיסמא:";
            // 
            // wsUserIdTextBox
            // 
            this.wsUserIdTextBox.Location = new System.Drawing.Point(16, 25);
            this.wsUserIdTextBox.Name = "wsUserIdTextBox";
            this.wsUserIdTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wsUserIdTextBox.Size = new System.Drawing.Size(177, 21);
            this.wsUserIdTextBox.TabIndex = 8;
            // 
            // wsPasswordTextBox
            // 
            this.wsPasswordTextBox.Location = new System.Drawing.Point(16, 56);
            this.wsPasswordTextBox.Name = "wsPasswordTextBox";
            this.wsPasswordTextBox.PasswordChar = '*';
            this.wsPasswordTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wsPasswordTextBox.Size = new System.Drawing.Size(177, 21);
            this.wsPasswordTextBox.TabIndex = 9;
            this.wsPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 391);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.FinishButton);
            this.Controls.Add(this.siteSettingsGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.Text = "SettingsWindow";
            this.siteSettingsGroupBox.ResumeLayout(false);
            this.siteSettingsGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox siteSettingsGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox enableTreatmentGroupsCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox wsIPtextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox virtualDirectoryTextBox;
        private System.Windows.Forms.Button loadConfiurationButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox wsPortTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox wsDISHtextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button FinishButton;
        private System.Windows.Forms.ComboBox appointmentLimitComboBox;
        private System.Windows.Forms.CheckBox appointmentLimitCheckBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox wsPasswordTextBox;
        private System.Windows.Forms.TextBox wsUserIdTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}