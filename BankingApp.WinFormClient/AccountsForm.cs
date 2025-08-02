using BankingApp.WinFormClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApp.WinFormClient;

public partial class AccountsForm : Form
{
    private readonly string _token;
    public AccountsForm(string token)
    {
        InitializeComponent();
        _token = token;
    }





    private async Task LoadAccounts()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7038");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var accounts = await client.GetFromJsonAsync<List<AccountModel>>("api/accounts");
        dgvAccounts.DataSource = accounts;
    }

    private async void btnLoadAccounts_Click(object sender, EventArgs e)
    {


        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7038");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        try
        {
            var accounts = await client.GetFromJsonAsync<List<AccountModel>>("api/accounts");
            dgvAccounts.DataSource = accounts;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Failed to load accounts: " + ex.Message;
        }
    }

    private async void btnCreateAccount_Click(object sender, EventArgs e)
    {
        //****
         

        var request = new NewAccountRequest
        {
            AccountType = txtAccountType.Text,
            InitialBalance = decimal.TryParse(txtInitialBalance.Text, out var amount) ? amount : 0
        };

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7038");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        try
        {
            var response = await client.PostAsJsonAsync("api/accounts", request);
            if (response.IsSuccessStatusCode)
            {
                lblMessage.Text = "Account created.";
                await LoadAccounts(); // reload accounts
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                lblMessage.Text = $"Failed: {response.StatusCode} - {error}";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    

















        //*****


    }
}




