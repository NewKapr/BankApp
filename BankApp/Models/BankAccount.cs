namespace BankApp.Models;

public abstract class BankAccount
{
    private readonly int _accountNumber;
    private string _owner = string.Empty;
    private decimal _balance;

    public int AccountNumber => _accountNumber;
    public decimal Balance => _balance;

    public string Owner
    {
        get => _owner;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя владельца не может быть пустым", nameof(value));
            _owner = value.Trim();
        }
    }

    protected BankAccount(int accountNumber, string owner, decimal initialBalance = 0m)
    {
        if (accountNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(accountNumber), "Номер счёта должен быть положительным");
        if (initialBalance < 0m)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Начальный баланс не может быть отрицательным");
        ValidatePrecision(initialBalance, nameof(initialBalance));

        _accountNumber = accountNumber;
        Owner = owner;
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        ValidateAmount(amount);
        _balance = checked(_balance + amount);
    }

    public void Withdraw(decimal amount)
    {
        ValidateAmount(amount);
        if (amount > _balance)
            throw new InvalidOperationException("Недостаточно средств на счёте");
        _balance -= amount;
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount <= 0m)
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма операции должна быть положительной");
        ValidatePrecision(amount, nameof(amount));
    }

    private static void ValidatePrecision(decimal amount, string parameterName)
    {
        if (decimal.Round(amount, 2) != amount)
            throw new ArgumentException("Сумма должна содержать не более двух знаков после запятой", parameterName);
    }

    public abstract void DisplayInfo();
}
