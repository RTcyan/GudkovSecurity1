using GudkovSecurity1.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GudkovSecurity1
{
    public partial class SettingsForm : Form
    {
        private const string PASSWORD_PATTERN = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$";

        private RepositoryService repositoryService = RepositoryService.getInstance();

        private User selectedUser = null;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            initDataGridView();
        }

        public void initDataGridView()
        {
            if (repositoryService.loginedUser.isAdmin)
            {
                dataGridView.Visible = true;
                toggleBlockButton.Visible = true;
                this.Height = 420;
                dataGridView.Columns.Add("login", "Логин");
                dataGridView.Columns.Add("isBlocked", "Блокировка");
                dataGridView.Columns.Add("id", "ID");
                dataGridView.Columns[2].Visible = false;
                refreshUsers();
            }
        }

        async private void refreshUsers()
        {
            dataGridView.Rows.Clear();
            List<User> users = await repositoryService.getUsers();
            foreach (User user in users)
            {
                dataGridView.Rows.Add(user.Login, user.isBlocked, user.Id);
            }
        }

        private void validatePasswords()
        {
            changePasswordButton.Enabled = false;
            userTextBox.ForeColor = Color.Red;
            if (!Regex.IsMatch(newPasswordTextBox.Text, PASSWORD_PATTERN))
            {
                userTextBox.Text = "Новый пароль должен состоять минимум из 6 символов. Должны присутствовать латинские символы с нижнем и верхнем регистре и цифры";
                return;
            }
            if (newPasswordTextBox.Text != repeatNewPasswordTextBox.Text)
            {
                userTextBox.Text = "Пароли не совпадают";
                return;
            }
            if (PasswordTextBox.Text == null)
            {
                userTextBox.Text = "Введите текущий пароль";
                return;
            }
            userTextBox.Text = "";
            changePasswordButton.Enabled = true;
        }

        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            validatePasswords();
        }

        private void repeatNewPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            validatePasswords();
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            validatePasswords();
        }

        async private void changePasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                await repositoryService.updatePassword(newPasswordTextBox.Text, PasswordTextBox.Text);
                PasswordTextBox.Text = "";

                userTextBox.ForeColor = Color.DarkGreen;
                userTextBox.Text = "Пароль сменен успешно";
                newPasswordTextBox.Text = "";
                repeatNewPasswordTextBox.Text = "";
            } catch (UncorrectPasswordException exception)
            {
                userTextBox.ForeColor = Color.Red;
                userTextBox.Text = exception.Message;
            }
        }

        async private void toggleBlockButton_Click(object sender, EventArgs e)
        {
            if (!repositoryService.loginedUser.isAdmin)
            {
                return;
            }
            try
            {
                await repositoryService.blockOrUnblockUser(new User()
                {
                    Id = selectedUser.Id,
                    isBlocked = !selectedUser.isBlocked,
                    isAdmin = selectedUser.isAdmin,
                    Login = selectedUser.Login,
                });
                refreshUsers();

            } catch (Exception exception)
            {
                if (typeof(UserNotFoundException).IsInstanceOfType(exception))
                {
                    blockUserLabel.ForeColor = Color.Red;
                    blockUserLabel.Text = exception.Message;
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView.Rows[e.RowIndex].Cells["id"].Value;
            if (id == null)
            {
                selectedUser = null;
            }
            else
            {
                selectedUser = new User()
                {
                    isAdmin = false,
                    Id = (int)id,
                    Login = (string)dataGridView.Rows[e.RowIndex].Cells["login"].Value,
                    isBlocked = (bool)dataGridView.Rows[e.RowIndex].Cells["isBlocked"].Value,
                };
            }
            toggleToggleBlockButtonState();
        }

        private void toggleToggleBlockButtonState()
        {
            if (repositoryService.loginedUser != null && repositoryService.loginedUser.isAdmin && selectedUser != null)
            {
                toggleBlockButton.Enabled = true;
                if (selectedUser.isBlocked)
                {
                    toggleBlockButton.Text = "Разблокировать";
                } else
                {
                    toggleBlockButton.Text = "Заблокировать";
                }
            }
            else
            {
                toggleBlockButton.Enabled = false;
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Заблокирован" : "Не заблокирован";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
