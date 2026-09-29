using BankApp.Models;

namespace BankApp.Services;

public class Bank
{
    private readonly List<BankAccount> _accounts = new();

    public void AddAccount(BankAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (FindAccount(account.AccountNumber) is not null)
            throw new InvalidOperationException("Счёт с таким номером уже существует");
        _accounts.Add(account);
    }

    public List<BankAccount> GetAllAccounts() => new(_accounts);

    public BankAccount? FindAccount(int accountNumber)
    {
        if (accountNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(accountNumber), "Номер счёта должен быть положительным");
        foreach (var account in _accounts)
        {
            if (account.AccountNumber == accountNumber)
                return account;
        }
        return null;
    }
}
