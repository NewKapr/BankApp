namespace BankApp.Models;

public class BankAccount
{
    public int AccountNumber;
    public string Owner = string.Empty;
    public decimal Balance;

    public void DisplayInfo()
    {
        Console.WriteLine($"Счёт №{AccountNumber}. Владелец: {Owner}. Баланс: {Balance:F2} руб.");
    }
}
