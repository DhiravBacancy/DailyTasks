using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Account
    {
        private string _name;
        private int _accNo;
        private int _balance;
        private List<string> _transactionHistory = new List<string>();
        private static List<Account> _accountList = new List<Account>();
        private static int idCounter = 1;
        private Account(string name, int accNo, int balance)
        {
            _name = name;
            _accNo = accNo;
            _balance = balance;
        }

        public static Account openAccount()
        {
            Console.WriteLine("Please enter details.... ");
            Console.Write("Account Name: "); 
            string name = Console.ReadLine();
            Console.Write("Initial balance : ");
            int balance = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");
            Account obj = new Account(name, Account.idCounter++, balance);
            _accountList.Add(obj);
            return obj;
        }

        public static void viewAllAccounts()
        {
            foreach (Account acc in _accountList)
            {
                Console.WriteLine($"Account Holder: {acc._name} Account No.: {acc._accNo} Balance: {acc._balance}");
            }
            Console.WriteLine("");
        }

        public static void searchByAccountName()
        {
            Console.WriteLine("Enter name you want to search");
            String accName = Console.ReadLine();
            Console.WriteLine("Accounts Found:");
            var foundAccounts = _accountList.FindAll(x => x._name.Contains(accName));
            foreach (var account in foundAccounts)
            {
                Console.WriteLine(account);
            }

            Console.WriteLine("");
        }

        // Order --- 0 for Ascending & 1 for Descending
        public static void sortAccByBalance()
        {
            Console.WriteLine("Enter 1 for ascending and 2 for descending");
            int order = Convert.ToInt32(Console.ReadLine());
            if (order == 1)
            {
                List<Account> sortedAscending = _accountList.OrderBy(a => a._balance).ToList();
                foreach (var l in sortedAscending)
                {
                    Console.WriteLine(l);
                }
            }
            else
            {
                List<Account> sortedDescending = _accountList.OrderByDescending(a => a._balance).ToList();
                foreach (var l in sortedDescending)
                {
                    Console.WriteLine(l);
                }
            }
            Console.WriteLine("");
            
        }

        public static void totalBankBalance()
        {
            int totalBalance = 0;
            foreach(var acc in _accountList)
            {
                totalBalance += acc._balance;
            }
            Console.WriteLine($"Total Balacnce in bank is : {totalBalance}");
            Console.WriteLine("");
        }

        public static void depositMoney(int accId)
        {
            Console.WriteLine("Enter Amount you want to Deposit ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Account Object = _accountList.Find(x => x._accNo == accId);
            Object._balance += amount;
            Object._transactionHistory.Add($"{amount} deposited to account, Updated Balance: {Object._balance}");
            Console.WriteLine("");
        }

        public static void withdrawMoney(int accId)
        {
            Console.WriteLine("Enter Amount you want to withdraw ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Account Object = _accountList.Find(x => x._accNo == accId);
            if (Object._balance - amount > 100)
            {
                Object._balance -= amount;
                Object._transactionHistory.Add($"{amount} withdrawed from account, Updated Balance: {Object._balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Balance to withdraw....");
            }
            Console.WriteLine("");
        }

        public static void transactionHistory(int accID)
        {
            Account Object = _accountList.Find(x => x._accNo == accID);
            foreach (string t in Object._transactionHistory)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine("");
        }

        public override string ToString()
        {
            return $"AccountHolder: {_name},Account Id : {_accNo}, Balance: {_balance}";
        }

    }
}
