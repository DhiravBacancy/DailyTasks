using System;
using System.Collections.Generic;

namespace OOPs_Day1
{
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
                string accno = "SAV001";

                foreach (var acc in accountsList)
                {
                    if (acc.AccountNumber == accno)
                    {
                        acc.DepositAmount = 100;
                    }
                }

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


                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during main execution: {ex.Message}");
            }
        }
    }

}
