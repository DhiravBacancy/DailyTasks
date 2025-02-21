using System;
using System.Linq.Expressions;

namespace Day5
{
    class Program
    {
        public static void Main()
        {
            try
            {
                while (true)
                {
                Jump:
                    Console.WriteLine("Select Any of below Choice by entering respective NUMBER....");
                    Console.WriteLine("1. Open Account ");
                    Console.WriteLine("2. View all opened Account ");
                    Console.WriteLine("3. Sort and display accounts by balance  ");
                    Console.WriteLine("4. Select an account ");
                    Console.WriteLine("5. View Details of an account ");
                    Console.WriteLine("6. Complete Bank balance of all accounts...");
                    Console.WriteLine("7. Clear Console");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                        {
                        case 1:
                            // code block
                            Account acc1 = Account.openAccount();
                            break;
                        case 2:
                            Account.viewAllAccounts();
                            // code block
                            break;
                        case 3:
                            Account.sortAccByBalance();
                            // code block
                            break;
                        case 4:
                            {
                                Console.WriteLine("Please enter id for selecting account ");
                                int accId = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Please Enter your Choice...");
                                Console.WriteLine("1. Deposit Money to account ");
                                Console.WriteLine("2. Withdraw Money to account ");
                                Console.WriteLine("3. View Transaction History of account ");
                                int ch = Convert.ToInt32(Console.ReadLine());
                                switch (ch)
                                {
                                    case 1:
                                        Account.depositMoney(accId);
                                        break;
                                    case 2:
                                        Account.withdrawMoney(accId);
                                        break;
                                    case 3:
                                        Account.transactionHistory(accId);
                                        break;
                                }
                            }
                            break;

                        case 5:

                            // code block
                            Account.searchByAccountName();
                            break;
                        case 6:                           
                            Account.totalBankBalance();
                            break;
                        case 7:
                            Console.Clear();
                            goto Jump;
                        default:
                            // code block
                            Console.WriteLine("Invalid Input");
                            Console.WriteLine("");
                            break;
                    }
                }             
            }
            catch (Exception err)
            {
                Console.WriteLine("Error Occured : " + err);
                throw;
            }
        }

    }
}

