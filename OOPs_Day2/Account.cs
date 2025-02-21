using System;
using System.Collections.Generic;

namespace OOPs_Day2
{
    // Sealed TransactionLogger class to track and log transactions
    public sealed class TransactionLogger
    {
        public void LogTransaction(Account acc)
        {
            try
            {
                foreach (string transaction in acc.TransactionHistory)
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine("");  // To add a blank line for readability
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging transaction: {ex.Message}");
            }
        }
    }


    public partial class AccountOperations 
    {
        //public AccountOperations(string accNo, string holderName, decimal balance) : base(accNo, holderName, balance) { }

        public decimal CheckBalance(Account acc)
        {
            try
            {
                return acc.Balance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking balance: {ex.Message}");
                return 0;
            }
        }
    }

    public class Account
    {
        private string _accountNumber;
        private string _holderName;
        private decimal _balance;
        private List<string> _transactionHistory = new List<string>();

        public string AccNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }
        public string HolderName
        {
            get { return _holderName; }
            set { _holderName = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public List<string> TransactionHistory
        {
            get { return _transactionHistory; }
        }

        protected Account(string accNo, string holderName, decimal balance)
        {
            try
            {
                _accountNumber = accNo;
                _holderName = holderName;
                _balance = balance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing account: {ex.Message}");
            }
        }

        public void AddTransaction(string transaction)
        {
            try
            {
                this._transactionHistory.Add(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding transaction: {ex.Message}");
            }
        }

        virtual public void DisplayAccountInfo() { }

        virtual public void CalculateInterest() { }
    }

    class SavingsAccount : Account
    {
        public SavingsAccount(string accNo, string holderName, decimal balance) : base(accNo, holderName, balance) { }

        public override void DisplayAccountInfo()
        {
            try
            {
                Console.WriteLine($"Account type: Savings Account, Account Number: {this.AccNumber}, Holder Name: {this.HolderName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying account info: {ex.Message}");
            }
        }

        public override void CalculateInterest()
        {
            try
            {
                Console.WriteLine($"Savings Account Interest: {this.Balance * 0.04m} (4% interest)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating interest: {ex.Message}");
            }
        }
    }

    class CurrentAccount : Account
    {
        public CurrentAccount(string accNo, string holderName, decimal balance) : base(accNo, holderName, balance) { }

        public override void DisplayAccountInfo()
        {
            try
            {
                Console.WriteLine($"Account type: Current Account, Account Number: {this.AccNumber}, Holder Name: {this.HolderName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying account info: {ex.Message}");
            }
        }

        public override void CalculateInterest()
        {
            try
            {
                Console.WriteLine("No interest for a Current Account.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating interest: {ex.Message}");
            }
        }
    }

}
