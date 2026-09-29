using System.Globalization;
using System.Text;
using BankApp.Models;
using BankApp.Services;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

var bank = new Bank();
bool running = true;

while (running)
{
    if (!Console.IsInputRedirected && !Console.IsOutputRedirected)
        Console.Clear();

    Console.WriteLine("=== Управление банковскими счетами ===");
    Console.WriteLine("1. Создать счёт");
    Console.WriteLine("2. Пополнить счёт");
    Console.WriteLine("3. Снять деньги");
    Console.WriteLine("4. Показать информацию о счёте");
    Console.WriteLine("5. Показать все счета");
    Console.WriteLine("6. Показать сберегательные счета");
    Console.WriteLine("0. Выход");

    try
    {
        int choice = ReadInteger("Выбор: ");
        switch (choice)
        {
            case 1: CreateAccount(bank); break;
            case 2: Deposit(bank); break;
            case 3: Withdraw(bank); break;
            case 4: FindAccount(bank).DisplayInfo(); break;
            case 5: ShowAccounts(bank.GetAllAccounts()); break;
            case 6: ShowAccounts(bank.GetSavingsAccounts()); break;
            case 0: running = false; break;
            default: Console.WriteLine("Неверный выбор."); break;
        }
    }
    catch (EndOfStreamException)
    {
        Console.WriteLine("Ввод завершён.");
        running = false;
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Ошибка: результат операции превышает допустимый числовой диапазон.");
    }

    if (running && !Console.IsInputRedirected)
    {
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey(intercept: true);
    }
}

static void CreateAccount(Bank bank)
{
    Console.WriteLine("1. Обычный счёт");
    Console.WriteLine("2. Сберегательный счёт");
    int type = ReadInteger("Тип счёта: ");
    if (type != 1 && type != 2)
        throw new ArgumentException("Неизвестный тип счёта");

    int number = ReadInteger("Номер счёта: ");
    if (bank.FindAccount(number) is not null)
        throw new InvalidOperationException("Счёт с таким номером уже существует");

    string owner = ReadText("Владелец: ");
    decimal initialBalance = ReadDecimal("Начальный баланс: ");
    BankAccount account;
    if (type == 1)
    {
        account = new RegularAccount(number, owner, initialBalance);
    }
    else
    {
        decimal rate = ReadDecimal("Процентная ставка (Enter — 5%): ", 5m);
        account = new SavingsAccount(number, owner, initialBalance, rate);
    }

    bank.AddAccount(account);
    Console.WriteLine($"Счёт №{account.AccountNumber} создан.");
}

static void Deposit(Bank bank)
{
    var account = FindAccount(bank);
    decimal amount = ReadDecimal("Сумма пополнения: ");
    account.Deposit(amount);
    Console.WriteLine($"Пополнение выполнено. Баланс: {account.Balance:F2} руб.");
}

static void Withdraw(Bank bank)
{
    var account = FindAccount(bank);
    decimal amount = ReadDecimal("Сумма снятия: ");
    account.Withdraw(amount);
    Console.WriteLine($"Снятие выполнено. Баланс: {account.Balance:F2} руб.");
}

static BankAccount FindAccount(Bank bank)
{
    int number = ReadInteger("Номер счёта: ");
    return bank.FindAccount(number)
        ?? throw new InvalidOperationException($"Счёт с номером {number} не найден");
}

static void ShowAccounts(IEnumerable<BankAccount> accounts)
{
    bool found = false;
    foreach (var account in accounts)
    {
        found = true;
        try
        {
            account.DisplayInfo();
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Счёт №{account.AccountNumber}. Владелец: {account.Owner}. Баланс: {account.Balance:F2} руб.");
            Console.WriteLine("Расчётный баланс с процентами превышает допустимый числовой диапазон.");
        }
    }
    if (!found)
        Console.WriteLine("Счета не найдены.");
}

static string ReadText(string prompt)
{
    Console.Write(prompt);
    string? input = Console.ReadLine();
    if (input is null)
        throw new EndOfStreamException();
    if (string.IsNullOrWhiteSpace(input))
        throw new ArgumentException("Поле не может быть пустым");
    return input.Trim();
}

static int ReadInteger(string prompt)
{
    string input = ReadText(prompt);
    if (!int.TryParse(input, out int value))
        throw new FormatException("Введите корректное целое число");
    return value;
}

static decimal ReadDecimal(string prompt, decimal? defaultValue = null)
{
    Console.Write(prompt);
    string? input = Console.ReadLine();
    if (input is null)
        throw new EndOfStreamException();
    if (string.IsNullOrWhiteSpace(input))
    {
        if (defaultValue.HasValue)
            return defaultValue.Value;
        throw new ArgumentException("Поле не может быть пустым");
    }

    string normalized = input.Trim().Replace(',', '.');
    if (!decimal.TryParse(normalized, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
        CultureInfo.InvariantCulture, out decimal value))
        throw new FormatException("Введите корректное число без разделителей тысяч");
    return value;
}
