using System.Globalization;
using System.Text;
using BankApp.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

BankAccount[] accounts =
[
    new RegularAccount(1001, "Иван Петров", 1000m),
    new SavingsAccount(1002, "Анна Смирнова", 2000m)
];
foreach (var account in accounts)
    account.DisplayInfo();

Console.WriteLine($"Реальный баланс сберегательного счёта: {accounts[1].Balance:F2} руб.");
