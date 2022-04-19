
namespace GudkovSecurity1
{
    partial class ContentForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userLabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.deleteRowButton = new System.Windows.Forms.Button();
            this.headerTextBox = new System.Windows.Forms.TextBox();
            this.textTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateRowButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(198, 15);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(0, 15);
            this.userLabel.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(198, 40);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(590, 398);
            this.dataGridView.TabIndex = 4;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // deleteRowButton
            // 
            this.deleteRowButton.Enabled = false;
            this.deleteRowButton.Location = new System.Drawing.Point(12, 12);
            this.deleteRowButton.Name = "deleteRowButton";
            this.deleteRowButton.Size = new System.Drawing.Size(180, 23);
            this.deleteRowButton.TabIndex = 6;
            this.deleteRowButton.Text = "Удалить строку";
            this.deleteRowButton.UseVisualStyleBackColor = true;
            this.deleteRowButton.Click += new System.EventHandler(this.deleteRowButton_Click);
            // 
            // headerTextBox
            // 
            this.headerTextBox.Location = new System.Drawing.Point(12, 41);
            this.headerTextBox.Name = "headerTextBox";
            this.headerTextBox.PlaceholderText = "Заголовок";
            this.headerTextBox.Size = new System.Drawing.Size(180, 23);
            this.headerTextBox.TabIndex = 7;
            this.headerTextBox.TextChanged += new System.EventHandler(this.headerTextBox_TextChanged);
            // 
            // textTextBox
            // 
            this.textTextBox.Location = new System.Drawing.Point(12, 70);
            this.textTextBox.Multiline = true;
            this.textTextBox.Name = "textTextBox";
            this.textTextBox.PlaceholderText = "Текст";
            this.textTextBox.Size = new System.Drawing.Size(180, 97);
            this.textTextBox.TabIndex = 8;
            this.textTextBox.TextChanged += new System.EventHandler(this.textTextBox_TextChanged);
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(12, 173);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(180, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Добавить запись";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateRowButton
            // 
            this.updateRowButton.Enabled = false;
            this.updateRowButton.Location = new System.Drawing.Point(12, 202);
            this.updateRowButton.Name = "updateRowButton";
            this.updateRowButton.Size = new System.Drawing.Size(180, 23);
            this.updateRowButton.TabIndex = 10;
            this.updateRowButton.Text = "Изменить запись";
            this.updateRowButton.UseVisualStyleBackColor = true;
            this.updateRowButton.Click += new System.EventHandler(this.changeRowButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(12, 415);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(180, 23);
            this.settingsButton.TabIndex = 11;
            this.settingsButton.Text = "Настройки";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // ContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.updateRowButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.textTextBox);
            this.Controls.Add(this.headerTextBox);
            this.Controls.Add(this.deleteRowButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.userLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ContentForm";
            this.Text = "Данные";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button deleteRowButton;
        private System.Windows.Forms.TextBox headerTextBox;
        private System.Windows.Forms.TextBox textTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateRowButton;
        private System.Windows.Forms.Button settingsButton;
    }
}

