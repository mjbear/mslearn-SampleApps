using System;

namespace BankAccountApp
{
    public class BankAccount
    {
        private readonly object balanceLock = new object();

        public string AccountNumber { get; }
        public double Balance { get; private set; }
        public string AccountHolderName { get; }
        public string AccountType { get; }
        public DateTime DateOpened { get; }

        public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");

            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            DateOpened = dateOpened;
        }

        public void Credit(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Credit amount cannot be negative.");

            lock (balanceLock)
            {
                Balance += amount;
            }
        }

        public void Debit(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Debit amount cannot be negative.");

            lock (balanceLock)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                }
                else
                {
                    throw new InsufficientBalanceException("Insufficient balance for debit.");
                }
            }
        }

        public void Transfer(BankAccount toAccount, double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Transfer amount cannot be negative.");

            lock (balanceLock)
            {
                if (Balance >= amount)
                {
                    if (AccountHolderName != toAccount.AccountHolderName && amount > 500)
                    {
                        throw new TransferLimitExceededException("Transfer amount exceeds maximum limit for different account owners.");
                    }

                    Debit(amount);
                    toAccount.Credit(amount);
                }
                else
                {
                    throw new InsufficientBalanceException("Insufficient balance for transfer.");
                }
            }
        }

        public double GetBalance()
        {
            lock (balanceLock)
            {
                return Balance;
            }
        }

        public void PrintStatement()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");
            // Add code here to print recent transactions
        }

        public double CalculateInterest(double interestRate)
        {
            if (interestRate < 0)
                throw new ArgumentException("Interest rate cannot be negative.");

            lock (balanceLock)
            {
                return Balance * interestRate;
            }
        }
    }
}