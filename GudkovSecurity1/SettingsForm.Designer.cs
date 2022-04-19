namespace GudkovSecurity1
{
    partial class SettingsForm
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
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.repeatNewPasswordTextBox = new System.Windows.Forms.TextBox();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toggleBlockButton = new System.Windows.Forms.Button();
            this.blockUserLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(12, 12);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.PlaceholderText = "Текущий пароль";
            this.PasswordTextBox.Size = new System.Drawing.Size(377, 23);
            this.PasswordTextBox.TabIndex = 5;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.Location = new System.Drawing.Point(12, 41);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.PasswordChar = '*';
            this.newPasswordTextBox.PlaceholderText = "Новый пароль";
            this.newPasswordTextBox.Size = new System.Drawing.Size(377, 23);
            this.newPasswordTextBox.TabIndex = 6;
            this.newPasswordTextBox.TextChanged += new System.EventHandler(this.newPasswordTextBox_TextChanged);
            // 
            // repeatNewPasswordTextBox
            // 
            this.repeatNewPasswordTextBox.Location = new System.Drawing.Point(12, 70);
            this.repeatNewPasswordTextBox.Name = "repeatNewPasswordTextBox";
            this.repeatNewPasswordTextBox.PasswordChar = '*';
            this.repeatNewPasswordTextBox.PlaceholderText = "Повторите новый пароль";
            this.repeatNewPasswordTextBox.Size = new System.Drawing.Size(377, 23);
            this.repeatNewPasswordTextBox.TabIndex = 7;
            this.repeatNewPasswordTextBox.TextChanged += new System.EventHandler(this.repeatNewPasswordTextBox_TextChanged);
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Enabled = false;
            this.changePasswordButton.Location = new System.Drawing.Point(12, 99);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(377, 23);
            this.changePasswordButton.TabIndex = 8;
            this.changePasswordButton.Text = "Подтвердить";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.changePasswordButton_Click);
            // 
            // userTextBox
            // 
            this.userTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.userTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userTextBox.ForeColor = System.Drawing.Color.Red;
            this.userTextBox.Location = new System.Drawing.Point(12, 128);
            this.userTextBox.Multiline = true;
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ReadOnly = true;
            this.userTextBox.Size = new System.Drawing.Size(377, 55);
            this.userTextBox.TabIndex = 9;
            this.userTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 189);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(374, 130);
            this.dataGridView.TabIndex = 10;
            this.dataGridView.Visible = false;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            // 
            // toggleBlockButton
            // 
            this.toggleBlockButton.Enabled = false;
            this.toggleBlockButton.Location = new System.Drawing.Point(12, 325);
            this.toggleBlockButton.Name = "toggleBlockButton";
            this.toggleBlockButton.Size = new System.Drawing.Size(377, 23);
            this.toggleBlockButton.TabIndex = 11;
            this.toggleBlockButton.Text = "Заблокировать";
            this.toggleBlockButton.UseVisualStyleBackColor = true;
            this.toggleBlockButton.Visible = false;
            this.toggleBlockButton.Click += new System.EventHandler(this.toggleBlockButton_Click);
            // 
            // blockUserLabel
            // 
            this.blockUserLabel.AutoSize = true;
            this.blockUserLabel.Location = new System.Drawing.Point(24, 362);
            this.blockUserLabel.Name = "blockUserLabel";
            this.blockUserLabel.Size = new System.Drawing.Size(0, 15);
            this.blockUserLabel.TabIndex = 12;
            this.blockUserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 182);
            this.Controls.Add(this.blockUserLabel);
            this.Controls.Add(this.toggleBlockButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.changePasswordButton);
            this.Controls.Add(this.repeatNewPasswordTextBox);
            this.Controls.Add(this.newPasswordTextBox);
            this.Controls.Add(this.PasswordTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.TextBox repeatNewPasswordTextBox;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button toggleBlockButton;
        private System.Windows.Forms.Label blockUserLabel;
    }
}