using System.Globalization;
using System.Text;
using BankApp.Models;
using BankApp.Services;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

var bank = new Bank();
bank.AddAccount(new RegularAccount(1001, "Иван Петров", 1000m));
bank.AddAccount(new SavingsAccount(1002, "Анна Смирнова", 2000m));

foreach (var account in bank.GetAllAccounts())
    account.DisplayInfo();

try
{
    bank.AddAccount(new RegularAccount(1001, "Павел Орлов"));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
