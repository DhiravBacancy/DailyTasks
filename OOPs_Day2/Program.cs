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
                savingsAccount.Withdraw(savingsWithdraw);
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
                Console.Write("Enter amount to withdraw from Current Account: ");
                decimal currentWithdraw = decimal.Parse(Console.ReadLine());
                currentAccount.Withdraw(currentWithdraw);
                Console.WriteLine($"Final Balance in Current Account: {currentAccount.Balance}\n");

                logger.LogTransaction(currentAccount);

                // User input for AccountOperations
                Console.Write("Enter AccountOperations Account Number: ");
                string accOpsNo = Console.ReadLine();
                Console.Write("Enter Holder Name: ");
                string accOpsHolderName = Console.ReadLine();
                Console.Write("Enter Initial Balance: ");
                decimal accOpsBalance = decimal.Parse(Console.ReadLine());

                AccountOperations accountOperations = new AccountOperations(accOpsNo, accOpsHolderName, accOpsBalance);
                Console.Write("Enter amount to deposit: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());
                accountOperations.Deposit(depositAmount);

                Console.Write("Enter amount to withdraw: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                accountOperations.Withdraw(withdrawAmount);

                Console.WriteLine($"Balance after operations: {accountOperations.CheckBalance()}\n");

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
