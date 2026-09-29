using System.Globalization;
using System.Text;
using BankApp.Models;
using BankApp.Services;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

var bank = new Bank();
bank.AddAccount(new RegularAccount(1001, "Иван Петров", 1000m));
bank.AddAccount(new SavingsAccount(1002, "Анна Смирнова", 2000m));

Console.WriteLine("=== Поиск счёта 1001 ===");
bank.FindAccount(1001)?.DisplayInfo();
Console.WriteLine(bank.FindAccount(9999) is null ? "Счёт 9999 не найден." : "Счёт найден.");

Console.WriteLine("=== Сберегательные счета ===");
foreach (var account in bank.GetSavingsAccounts())
    account.DisplayInfo();
