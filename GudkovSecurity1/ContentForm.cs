using GudkovSecurity1.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GudkovSecurity1
{
    public partial class ContentForm : Form
    {
        private RepositoryService repositoryService = RepositoryService.getInstance();

        private User loginedUser;

        private Post selectedPost = null;

        private SettingsForm settingsForm = null;

        public ContentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initDataGridView();

            this.loginedUser = repositoryService.loginedUser;

            if (loginedUser == null)
            {
                Application.Exit();
            }

            refreshData();
            updateUserInfo();



            if (loginedUser.isAdmin)
            {
                deleteRowButton.Enabled = true;
            }
        }

        public void updateUserInfo()
        {
#if DEBUG
            userLabel.ForeColor = Color.Red;
            userLabel.Text = "Автоматический вход";
#else
            userLabel.Text = "Залогиненный пользователь: " + loginedUser.Login + " (" + (loginedUser.isAdmin ? "Админ" : "Пользователь") + ")";
#endif
        }

        public void initDataGridView()
        {
            dataGridView.Columns.Add("header", "Заголовок");
            dataGridView.Columns.Add("text", "Текст");
            dataGridView.Columns.Add("id", "ID");
            dataGridView.Columns[2].Visible = false;
        }

        async private void refreshData()
        {
            dataGridView.Rows.Clear();
            List<Post> posts = await repositoryService.getData();
            foreach (Post post in posts)
            {
                dataGridView.Rows.Add(post.Header, post.Text, post.Id);
            }
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (loginedUser == null)
            {
                userLabel.ForeColor = Color.Red;
                userLabel.Text = "Невозможно удалить данные! Пользователь должен войти в систему!";
                return;
            }
            if (!loginedUser.isAdmin)
            {
                userLabel.ForeColor = Color.Red;
                userLabel.Text = "Невозможно удалить данные! Недостаточно данных!";
                return;
            }
            if (selectedPost == null) {
                userLabel.ForeColor = Color.Red;
                userLabel.Text = "Удаление невозможно! Необходимо выбрать строку";
                return;
            }

            updateUserInfo();

            repositoryService.deleteRow(selectedPost.Id);
            refreshData();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView.Rows[e.RowIndex].Cells["id"].Value;
            if (id == null)
            {
                selectedPost = null;
            } else
            {
                selectedPost = new Post()
                {
                    Header = (string)dataGridView.Rows[e.RowIndex].Cells["header"].Value,
                    Id = (int)id,
                    Text = (string)dataGridView.Rows[e.RowIndex].Cells["text"].Value,
                };
            }
            toggleDeleteRowButtonState();
            toggleUpdateRowButtonState();
        }

        private void toggleDeleteRowButtonState()
        {
            if (loginedUser != null && loginedUser.isAdmin && selectedPost != null)
            {
                deleteRowButton.Enabled = true;
            }
            else
            {
                deleteRowButton.Enabled = false;
            }
        }

        private void toggleUpdateRowButtonState()
        {
            if (loginedUser != null && loginedUser.isAdmin && selectedPost != null)
            {
                updateRowButton.Enabled = true;
                headerTextBox.Text = selectedPost.Header;
                textTextBox.Text = selectedPost.Text;
            }
            else
            {
                deleteRowButton.Enabled = false;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string header = headerTextBox.Text;
            string text = textTextBox.Text;
            repositoryService.addRow(header, text);
            textTextBox.Text = "";
            headerTextBox.Text = "";
            refreshData();
        }

        private void textTextBox_TextChanged(object sender, EventArgs e)
        {
            toggleAddButtonState();
        }

        private void headerTextBox_TextChanged(object sender, EventArgs e)
        {
            toggleAddButtonState();
        }

        private void toggleAddButtonState()
        {
            if (textTextBox.Text == null || textTextBox.Text.Length == 0)
            {
                addButton.Enabled = false;
                return;
            }
            if (headerTextBox.Text == null || textTextBox.Text.Length == 0)
            {
                addButton.Enabled = false;
                return;
            }
            addButton.Enabled = true;
        }

        private void changeRowButton_Click(object sender, EventArgs e)
        {
            if (!updateRowButton.Enabled)
            {
                return;
            }
            Post updatedPost = new Post()
            {
                Id = selectedPost.Id,
                Header = headerTextBox.Text,
                Text = textTextBox.Text,
            };
            repositoryService.updatePost(updatedPost);
            textTextBox.Text = "";
            headerTextBox.Text = "";
            toggleUpdateRowButtonState();
            refreshData();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settingsForm != null)
            {
                return;
            }
            settingsForm = new SettingsForm();
            settingsForm.Show();
        }
    }
}
