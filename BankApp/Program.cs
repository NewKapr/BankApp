using System.Globalization;
using System.Text;
using BankApp.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

var account = new BankAccount
{
    AccountNumber = 1001,
    Owner = "Иван Петров",
    Balance = 1000m
};
account.DisplayInfo();
