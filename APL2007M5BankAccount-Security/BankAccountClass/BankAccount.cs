﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace BankAccountApp
{
    public class InsufficientFundsException : Exception
    {
        public double AttemptedAmount { get; }
        public double CurrentBalance { get; }

        public InsufficientFundsException(double attemptedAmount, double currentBalance)
            : base($"Insufficient balance for debit. Attempted to debit {attemptedAmount}, but current balance is {currentBalance}.")
        {
            AttemptedAmount = attemptedAmount;
            CurrentBalance = currentBalance;
        }
    }

    public class InvalidAccountTypeException : Exception
    {
        public string AccountType { get; }

        public InvalidAccountTypeException(string accountType)
            : base($"Invalid account type: {accountType}.")
        {
            AccountType = accountType;
        }
    }

    public class InvalidAccountNumberException : Exception
    {
        public string AccountNumber { get; }

        public InvalidAccountNumberException(string accountNumber)
            : base($"Invalid account number: {accountNumber}.")
        {
            AccountNumber = accountNumber;
        }
    }

    public class InvalidAccountHolderNameException : Exception
    {
        public string AccountHolderName { get; }

        public InvalidAccountHolderNameException(string accountHolderName)
            : base($"Invalid account holder name: {accountHolderName}.")
        {
            AccountHolderName = accountHolderName;
        }
    }

    public class InvalidDateOpenedException : Exception
    {
        public DateTime DateOpened { get; }

        public InvalidDateOpenedException(DateTime dateOpened)
            : base($"Invalid date opened: {dateOpened}.")
        {
            DateOpened = dateOpened;
        }
    }

    public class InvalidInitialBalanceException : Exception
    {
        public double InitialBalance { get; }

        public InvalidInitialBalanceException(double initialBalance)
            : base($"Invalid initial balance: {initialBalance}.")
        {
            InitialBalance = initialBalance;
        }
    }

    public class InvalidInterestRateException : Exception
    {
        public double InterestRate { get; }

        public InvalidInterestRateException(double interestRate)
            : base($"Invalid interest rate: {interestRate}.")
        {
            InterestRate = interestRate;
        }
    }

    public class InvalidTransferAmountException : Exception
    {
        public double TransferAmount { get; }

        public InvalidTransferAmountException(double transferAmount)
            : base($"Invalid transfer amount: {transferAmount}.")
        {
            TransferAmount = transferAmount;
        }
    }

    public class InvalidCreditAmountException : Exception
    {
        public double CreditAmount { get; }

        public InvalidCreditAmountException(double creditAmount)
            : base($"Invalid credit amount: {creditAmount}.")
        {
            CreditAmount = creditAmount;
        }
    }

    public class InvalidDebitAmountException : Exception
    {
        public double DebitAmount { get; }

        public InvalidDebitAmountException(double debitAmount)
            : base($"Invalid debit amount: {debitAmount}.")
        {
            DebitAmount = debitAmount;
        }
    }


    public class BankAccount
    {
        public enum AccountTypes
        {
            Savings,
            Checking,
            MoneyMarket,
            CertificateOfDeposit,
            Retirement
        }
        public string AccountNumber { get; }
        public double Balance { get; private set; }
        public string AccountHolderName { get; }
        public AccountTypes AccountType { get; }
        public DateTime DateOpened { get; }
        private const double MaxTransferAmountForDifferentOwners = 500;
        
        public string Username { get; }
        // private string PasswordHash { get; }
        // yes, this is insecure
        public string PasswordHash { get; }

        public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened, string username, string password)
        {
            if (accountNumber.Length != 10)
            {
                throw new InvalidAccountNumberException(accountNumber);
            }
        
            if (initialBalance < 0)
            {
                throw new InvalidInitialBalanceException(initialBalance);
            }
        
            if (accountHolderName.Length < 2)
            {
                throw new InvalidAccountHolderNameException(accountHolderName);
            }
        
            /* the enum will enforce the valid values
            if (accountType != "Savings" && accountType != "Checking" && accountType != "Money Market" && accountType != "Certificate of Deposit" && accountType != "Retirement")
            {
                throw new InvalidAccountTypeException(accountType);
            }
            */     
        
            if (dateOpened > DateTime.Now)
            {
                throw new InvalidDateOpenedException(dateOpened);
            }
        
            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountHolderName = accountHolderName;
            //AccountType = AccountTypes.Savings; // (AccountTypes)Enum.Parse(typeof(AccountTypes), accountType);
            AccountType = (AccountTypes)Enum.Parse(typeof(AccountTypes), accountType);
            DateOpened = dateOpened;
            Username = username;
            PasswordHash = HashPassword(password);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool Authenticate(string username, string password)
        {
            return Username == username && PasswordHash == password;
        }

        public void Credit(double amount, string username, string password)
        {
            try
            {
                if (!Authenticate(username, password))
                {
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                if (amount < 0)
                {
                    throw new InvalidCreditAmountException(amount);
                }

                Balance += amount;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogException(ex);
                throw new UnauthorizedAccessException("Authentication failed.");
            }
            catch (InvalidCreditAmountException ex)
            {
                LogException(ex);
                throw new InvalidCreditAmountException(amount);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("An unexpected error occurred while crediting the account.");
            }
        }

        public void Debit(double amount, string username, string password)
        {
            try
            {
                if (!Authenticate(username, password))
                {
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                if (amount < 0)
                {
                    throw new InvalidDebitAmountException(amount);
                }

                if (Balance >= amount)
                {
                    Balance -= amount;
                }
                else
                {
                    throw new InsufficientFundsException(amount, Balance);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                LogException(ex);
                throw new UnauthorizedAccessException("Authentication failed.");
            }
            catch (InvalidDebitAmountException ex)
            {
                LogException(ex);
                throw new InvalidDebitAmountException(amount);
            }
            catch (InsufficientFundsException ex)
            {
                LogException(ex);
                throw new InsufficientFundsException(amount, Balance);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("An unexpected error occurred while debiting the account.");
            }
        }
        
        public void Transfer(BankAccount toAccount, double amount, string username, string password)
        {
            try
            {
                if (!Authenticate(username, password))
                {
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                ValidateTransferAmount(amount);
                ValidateTransferLimitForDifferentOwners(toAccount, amount);

                if (Balance >= amount)
                {
                    Debit(amount, username, password);
                    toAccount.Credit(amount, toAccount.Username, toAccount.PasswordHash);
                }
                else
                {
                    throw new InsufficientFundsException(amount, Balance);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                LogException(ex);
                throw new UnauthorizedAccessException("Authentication failed.");
            }
            catch (InvalidTransferAmountException ex)
            {
                LogException(ex);
                throw new InvalidTransferAmountException(amount);
            }
            catch (InsufficientFundsException ex)
            {
                LogException(ex);
                throw new InsufficientFundsException(amount, Balance);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("An unexpected error occurred while transferring the amount.");
            }
        }

        private void LogException(Exception ex)
        {
            // Implement your logging mechanism here
            // For example, log to a file or a logging service
            Console.WriteLine($"Exception: {ex.Message}");
        }

        public double GetBalance()
        {
            return Balance; // Math.Round(balance, 2);
        }

        private void ValidateTransferAmount(double amount)
        {
            if (amount < 0)
            {
                throw new InvalidTransferAmountException(amount);
            }
        }

        private void ValidateTransferLimitForDifferentOwners(BankAccount toAccount, double amount)
        {
            if (AccountHolderName != toAccount.AccountHolderName && amount > MaxTransferAmountForDifferentOwners)
            {
                throw new Exception("Transfer amount exceeds maximum limit for different account owners.");
            }
        }


        /* 
        
        public void Transfer(BankAccount toAccount, double amount)
        {
            if (amount < 0)
            {
                throw new InvalidTransferAmountException(amount);
            }

            if (Balance >= amount)
            {
                if (AccountHolderName != toAccount.AccountHolderName && amount > 500)
                {
                    throw new Exception("Transfer amount exceeds maximum limit for different account owners.");
                }

                Debit(amount);
                toAccount.Credit(amount);
            }
            else
            {
                throw new Exception("Insufficient balance for transfer.");
            }
        }

        */

        public void PrintStatement()
        {
            Console
                .WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");

            // Add code here to print recent transactions
        }

        public double CalculateInterest(double interestRate)
        {
            if (interestRate < 0)
            {
                throw new InvalidInterestRateException(interestRate);
            }

            return Balance * interestRate;
        }
    }

}