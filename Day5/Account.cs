using System;
using System.Collections.Generic;
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
        List<Account> list = new List<Account>();

        public Account(string name, int accNo, int balance)
        {
            _name = name;
            _accNo = accNo;
            _balance = balance;
        }
        public static void storeObject(Account acc)
        {
            list.add(acc);
        }

        static public void viewAllAccounts()
        {

        }

        static public void searchByAccountName()
        {

        }

        // Order --- 0 for Ascending & 1 for Descending
        static public void sortAccByBalance(int order)
        {

        }
        static public void totalBankBalance()
        {

        }

        public void depositMoney()
        {

        }

        public void withdrawMoney()
        {

        }

        public void transactionHistory()
        {

        }

    }
}
