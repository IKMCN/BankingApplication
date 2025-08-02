using BankingApp.WinFormClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApp.WinFormClient
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            var model = new AuthenticationModel
            {
                Username = txtUsername.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7038");

            try
            {
                var response = await client.PostAsJsonAsync("api/authentication/register", model);
                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Registration successful!";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    lblMessage.Text = $"Registration failed: {response.StatusCode} - {error}";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

    }
}
