using System;
using System.Collections.Generic;

class BankAccount : IDisposable
{
    private string _accountNumber;
    private string _holderName;
    private decimal _balance;

    public string AccountNumber => _accountNumber;
    public string HolderName => _holderName;
    public decimal Balance => _balance;  // Read-only balance

    // Constructor
    public BankAccount(string accountNumber, string holderName, decimal initialBalance)
    {
        _accountNumber = accountNumber;
        _holderName = holderName;
        _balance = initialBalance;
        Console.WriteLine($"Account Created: {accountNumber}, Holder: {holderName}, Balance: {initialBalance}");
    }

    // Deposit method
    public void Deposit(decimal amount)
    {
        try
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            _balance += amount;
            Console.WriteLine($"Deposited: {amount}. New Balance: {_balance}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Withdraw method
    public void Withdraw(decimal amount)
    {
        try
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");
            if (amount > _balance)
                throw new InvalidOperationException("Insufficient balance.");

            _balance -= amount;
            Console.WriteLine($"Withdrawn: {amount}. New Balance: {_balance}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Property-based deposit
    public decimal DepositAmount
    {
        set { Deposit(value); }  // Calls Deposit() inside setter
    }

    // Property-based withdrawal
    public decimal WithdrawAmount
    {
        set { Withdraw(value); }  // Calls Withdraw() inside setter
    }

    // Dispose method to handle cleanup
    public void Dispose()
    {
        try
        {
            // Cleanup code
            Console.WriteLine($"Account {AccountNumber} is closed.");
            GC.SuppressFinalize(this);  // Suppress finalization since we handled it manually
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during disposal: {ex.Message}");
        }
    }

    // Destructor (optional, as we use IDisposable)
    ~BankAccount()
    {
        try
        {
            Console.WriteLine($"Destructor for Account {AccountNumber} is being called.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during finalization: {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            // Creating multiple accounts
            List<BankAccount> accountsList = new List<BankAccount>
            {
                new BankAccount("SAV001", "Alice", 5000),
                new BankAccount("SAV002", "Bob", 7000),
                new BankAccount("SAV003", "Charlie", 10000),
                new BankAccount("CUR001", "David", 15000),
                new BankAccount("CUR002", "Eve", 20000)
            };

            // Performing Transactions
            accountsList[0].DepositAmount = 2000.0m;    // Alice deposits 2000
            accountsList[1].WithdrawAmount = 3000.0m;   // Bob withdraws 3000
            accountsList[2].WithdrawAmount = 12000.0m;  // Charlie tries to withdraw more than balance (invalid)
            accountsList[3].DepositAmount = 5000.0m;    // David deposits 5000
            accountsList[4].WithdrawAmount = 5000.0m;   // Eve withdraws 5000

            // Display Final Balances
            foreach (var account in accountsList)
            {
                Console.WriteLine($"Account: {account.AccountNumber}, Holder: {account.HolderName}, Balance: {account.Balance}");
            }

            // Explicitly disposing accounts
            foreach (var account in accountsList)
            {
                account.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during main execution: {ex.Message}");
        }
    }
}
