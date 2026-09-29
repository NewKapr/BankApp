namespace BankApp.Models;

public class SavingsAccount : BankAccount
{
    private decimal _interestRate;

    public decimal InterestRate
    {
        get => _interestRate;
        set
        {
            if (value < 0m || value > 100m)
                throw new ArgumentOutOfRangeException(nameof(value), "Процентная ставка должна быть от 0 до 100");
            _interestRate = value;
        }
    }

    public decimal ProjectedBalance => decimal.Round(
        Balance + Balance * (InterestRate / 100m), 2, MidpointRounding.AwayFromZero);

    public SavingsAccount(int accountNumber, string owner, decimal initialBalance = 0m,
        decimal interestRate = 5m) : base(accountNumber, owner, initialBalance)
    {
        InterestRate = interestRate;
    }

    public override void DisplayInfo()
    {
        decimal projectedBalance = ProjectedBalance;
        Console.WriteLine($"Сберегательный счёт №{AccountNumber}. Владелец: {Owner}.");
        Console.WriteLine($"Баланс: {Balance:F2} руб. Ставка: {InterestRate}%.");
        Console.WriteLine($"Расчётный баланс с процентами: {projectedBalance:F2} руб.");
    }
}
