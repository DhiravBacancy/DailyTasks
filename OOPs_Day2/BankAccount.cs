namespace OOPs_Day2
{
    class BankAccount : BankServices, IBankingOperations
    {
        private string _accountNumber;
        private string _holderName;
        private decimal _balance;

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

        public BankAccount(string accNo, string holderName, decimal balance)
        {
            try
            {
                if (balance < 0)
                    throw new ArgumentException("Initial balance cannot be negative.");
                _accountNumber = accNo;
                _holderName = holderName;
                _balance = balance;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public override void LoanApprovalStatus()
        {
            Console.WriteLine("Loan Not approved");
        }

        void IBankingOperations.Deposit(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Deposit amount must be positive.");

                Balance += amount;
                Console.WriteLine($"Deposited: {amount}. New Balance: {Balance}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        void IBankingOperations.Withdraw(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Withdrawal amount must be positive.");
                if (amount > this.Balance)
                    throw new InvalidOperationException("Insufficient balance.");

                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New Balance: {Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        decimal IBankingOperations.GetBalance()
        {
            return Balance;
        }
    }

    public abstract class BankServices
    {
        abstract public void LoanApprovalStatus();
        public void PrintBankDetails()
        {
            Console.WriteLine("HDFC Bank.....");
        }
    }

    interface IBankingOperations
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal GetBalance();
    }

    public partial class AccountOperations
    {
        public void Deposit(Account acc, decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Deposit amount must be positive.");
                acc.Balance += amount;
                Console.WriteLine($"Deposited: {amount}. New Balance: {acc.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Withdraw(Account acc, decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Withdrawal amount must be positive.");
                if (amount > acc.Balance)
                    throw new InvalidOperationException("Insufficient balance.");
                acc.Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New Balance: {acc.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}