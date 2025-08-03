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
    private int _selectedAccountId;
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
    }

    private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            _selectedAccountId = Convert.ToInt32(dgvAccounts.Rows[e.RowIndex].Cells["AccountId"].Value);
            lblSelectedAccountId.Text = $"Selected Account ID: {_selectedAccountId}";
        }
    }

    private async void btnDeposit_Click(object sender, EventArgs e)
    {
        await HandleTransaction("deposit");
    }

    private async void btnWithdraw_Click(object sender, EventArgs e)
    {
        await HandleTransaction("withdraw");
    }

    private async Task HandleTransaction(string type)
    {
        if (_selectedAccountId == 0)
        {
            lblTransactionMessage.Text = "Please select an account.";
            return;
        }

        if (!decimal.TryParse(txtAmount.Text, out var amount) || amount <= 0)
        {
            lblTransactionMessage.Text = "Enter a valid amount.";
            return;
        }

        var request = new TransactionRequest
        {
            AccountId = _selectedAccountId,
            Amount = amount
        };

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7038");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        try
        {
            var url = $"api/accounts/{_selectedAccountId}/{type}";
            var response = await client.PostAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TransactionResponse>();
                lblTransactionMessage.Text = $"{type} successful. New Balance: £{result?.NewBalance}";
                await LoadAccounts(); // refresh grid
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                lblTransactionMessage.Text = $"Error: {error}";
            }
        }
        catch (Exception ex)
        {
            lblTransactionMessage.Text = $"Exception: {ex.Message}";
        }
    }
}




