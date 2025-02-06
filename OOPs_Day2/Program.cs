using System;

namespace OOPs_Day2
{
    public class MainClass
    {
        public static void Main(String[] args)
        {
            try
            {
                // Create a Transaction Logger
                TransactionLogger logger = new TransactionLogger();
                AccountOperations accountOperations = new AccountOperations();

                // User input for Savings Account
                Console.Write("Enter Savings Account Number: ");
                string savingsAccNo = Console.ReadLine();
                Console.Write("Enter Holder Name: ");
                string savingsHolderName = Console.ReadLine();
                Console.Write("Enter Initial Balance: ");
                decimal savingsBalance = decimal.Parse(Console.ReadLine());
                SavingsAccount savingsAccount = new SavingsAccount(savingsAccNo, savingsHolderName, savingsBalance);
                savingsAccount.DisplayAccountInfo();
                savingsAccount.CalculateInterest();
                Console.Write("Enter amount to withdraw from Savings Account: ");
                decimal savingsWithdraw = decimal.Parse(Console.ReadLine());
                accountOperations.Withdraw(savingsAccount, savingsWithdraw);
                Console.Write("Enter amount to withdraw from Savings Account: ");
                decimal savingsDeposit = decimal.Parse(Console.ReadLine());
                accountOperations.Withdraw(savingsAccount, savingsDeposit);
                Console.WriteLine($"Final Balance in Savings Account: {savingsAccount.Balance}\n");
                logger.LogTransaction(savingsAccount);


                // User input for Current Account
                Console.Write("Enter Current Account Number: ");
                string currentAccNo = Console.ReadLine();
                Console.Write("Enter Holder Name: ");
                string currentHolderName = Console.ReadLine();
                Console.Write("Enter Initial Balance: ");
                decimal currentBalance = decimal.Parse(Console.ReadLine());
                CurrentAccount currentAccount = new CurrentAccount(currentAccNo, currentHolderName, currentBalance);
                currentAccount.DisplayAccountInfo();
                currentAccount.CalculateInterest();
                Console.Write("Enter amount to withdraw from Savings Account: ");
                decimal currentWithdraw = decimal.Parse(Console.ReadLine());
                accountOperations.Withdraw(currentAccount, currentWithdraw);
                Console.Write("Enter amount to withdraw from Savings Account: ");
                decimal currentDeposit = decimal.Parse(Console.ReadLine());
                accountOperations.Withdraw(currentAccount, currentDeposit);
                logger.LogTransaction(currentAccount);



                // User input for BankAccount with IBankingOperations
                Console.Write("Enter Bank Account Number: ");
                string bankAccNo = Console.ReadLine();
                Console.Write("Enter Holder Name: ");
                string bankHolderName = Console.ReadLine();
                Console.Write("Enter Initial Balance: ");
                decimal bankBalance = decimal.Parse(Console.ReadLine());

                BankAccount account = new BankAccount(bankAccNo, bankHolderName, bankBalance);
                IBankingOperations bankingOperations = account;

                Console.Write("Enter amount to deposit: ");
                decimal bankDeposit = decimal.Parse(Console.ReadLine());
                bankingOperations.Deposit(bankDeposit);

                Console.Write("Enter amount to withdraw: ");
                decimal bankWithdraw = decimal.Parse(Console.ReadLine());
                bankingOperations.Withdraw(bankWithdraw);

                decimal finalBalance = bankingOperations.GetBalance();
                Console.WriteLine($"Current Balance: {finalBalance}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input. Please enter numeric values where required.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
