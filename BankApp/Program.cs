using System.Globalization;
using System.Text;
using BankApp.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

var account = new BankAccount(1001, "Иван Петров", 1000m);
account.Deposit(250m);
account.Withdraw(100m);
account.DisplayInfo();

try
{
    account.Withdraw(2000m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
account.DisplayInfo();
