using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.WinFormClient.Models;

public class NewAccountRequest
{
    public string AccountType { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}