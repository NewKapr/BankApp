namespace BankApp.Models;

public class RegularAccount : BankAccount
{
    public RegularAccount(int accountNumber, string owner, decimal initialBalance = 0m)
        : base(accountNumber, owner, initialBalance)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Обычный счёт №{AccountNumber}. Владелец: {Owner}. Баланс: {Balance:F2} руб.");
    }
}
