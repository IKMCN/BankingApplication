using System.Net.Http.Headers;
using System.Net.Http.Json;
using BankingApp.WinFormClient.Models;



namespace BankingApp.WinFormClient
{

    public partial class LoginForm : Form
    {
        private string _jwtToken = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var model = new
            {
                username = txtUsername.Text,
                password = txtPassword.Text
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7038");

            try
            {
                var response = await client.PostAsJsonAsync("api/authentication/login", model);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<TokenModel>();
                    _jwtToken = token?.Token ?? "";
                    lblMessage.Text = "Login successful!";
                    this.Hide();
                    var dashboard = new AccountsForm(_jwtToken);
                    dashboard.Show();
                }
                else
                {
                    lblMessage.Text = "Login failed.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
