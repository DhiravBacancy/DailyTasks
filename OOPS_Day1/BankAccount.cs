using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPs_Day1
{
    class BankAccount
    {
        private string _accountNumber;
        private string _holderName;
        private decimal _balance;

        // Properties using normal get accessor
        public string AccountNumber
        {
            get { return _accountNumber; }
        }

        public string HolderName
        {
            get { return _holderName; }
        }

        public decimal Balance
        {
            get { return GetBalance(); }
        }

        // Constructor
        public BankAccount(string accountNumber, string holderName, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _holderName = holderName;
            _balance = initialBalance;
            Console.WriteLine($"Account Created: {accountNumber}, Holder: {holderName}, Balance: {initialBalance}");
        }

        // Method to return balance (used in the get accessor)
        public decimal GetBalance()
        {
            return _balance;
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

        // Destructor
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
}
