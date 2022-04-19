using GudkovSecurity1.models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GudkovSecurity1
{
    public partial class AuthForm : Form
    {
        private RepositoryService repositoryService = RepositoryService.getInstance();

        public AuthForm()
        {
            InitializeComponent();
        }

        private async void AutoLogin()
        {
            LoginTextBox.Text = "admin";
            PasswordTextBox.Text = "Admin123";
            await Task.Delay(100);
            LoginButton.PerformClick();
        }

        async private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!LoginButton.Enabled)
            {
                return;
            }
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            User user = null;
            try
            {
                user = await repositoryService.login(login, password);
            } catch (Exception ex)
            {
                if (typeof(UserWasBlockedException).IsInstanceOfType(ex))
                {
                    userTextBox.ForeColor = Color.Red;
                    userTextBox.Text = ex.Message;
                    return;
                } else
                {
                    throw;
                }
            }
            if (user != null)
            {
                ContentForm contentForm = new ContentForm();
                contentForm.Show();
                contentForm.Closed += (s, args) => this.Close();
                this.Hide();
            }
            else
            {
                userTextBox.ForeColor = SystemColors.ControlText;
                userTextBox.Text = "Неверные данные!" ;
            }
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            AutoLogin();
#endif
        }
    }
}
