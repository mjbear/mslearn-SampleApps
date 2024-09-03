using BankAccountApp;
using System;

namespace BankAccountUnitTests
{
    public class BankAccountTests
    {
        [Fact]
        public void Credit_WithPositiveAmount_UpdatesBalance()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            account.Credit(50, "jdoe", "jdoe123");
            Assert.Equal(150, account.GetBalance());
        }

        [Fact]
        public void Credit_WithNegativeAmount_ShouldThrowException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentException>(() => account.Credit(-50, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Credit_WithZeroAmount_ShouldNotChangeBalance()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            account.Credit(0, "jdoe", "jdoe123");
            Assert.Equal(100, account.GetBalance());
        }

        [Fact]
        public void Debit_WithSufficientBalance_ReducesBalance()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            account.Debit(50, "jdoe", "jdoe123");
            Assert.Equal(50, account.GetBalance());
        }

        [Fact]
        public void Debit_WithInsufficientBalance_ThrowsException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<Exception>(() => account.Debit(150, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Debit_WithNegativeAmount_ShouldThrowException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentException>(() => account.Debit(-50, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Debit_WithZeroAmount_ShouldNotChangeBalance()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            account.Debit(0, "jdoe", "jdoe123");
            Assert.Equal(100, account.GetBalance());
        }

        [Fact]
        public void Transfer_WithSufficientBalance_ShouldDecreaseSourceAndIncreaseTargetBalance()
        {
            var sourceAccount = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var targetAccount = new BankAccount("67890", 100, "Jane Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            sourceAccount.Transfer(targetAccount, 50, "jdoe", "jdoe123");
            Assert.Equal(50, sourceAccount.GetBalance());
            Assert.Equal(150, targetAccount.GetBalance());
        }

        [Fact]
        public void Transfer_WithInsufficientBalance_ShouldThrowException()
        {
            var sourceAccount = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var targetAccount = new BankAccount("67890", 100, "Jane Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<Exception>(() => sourceAccount.Transfer(targetAccount, 150, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Transfer_WithNegativeAmount_ShouldThrowException()
        {
            var sourceAccount = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var targetAccount = new BankAccount("67890", 100, "Jane Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentException>(() => sourceAccount.Transfer(targetAccount, -50, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Transfer_WithZeroAmount_ShouldNotChangeBalances()
        {
            var sourceAccount = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var targetAccount = new BankAccount("67890", 100, "Jane Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            sourceAccount.Transfer(targetAccount, 0, "jdoe", "jdoe123");
            Assert.Equal(100, sourceAccount.GetBalance());
            Assert.Equal(100, targetAccount.GetBalance());
        }

        [Fact]
        public void Transfer_ToSameAccount_ShouldThrowException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentException>(() => account.Transfer(account, 50, "jdoe", "jdoe123"));
        }

        [Fact]
        public void Transfer_ToNullAccount_ShouldThrowException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentNullException>(() => account.Transfer(null, 50, "jdoe", "jdoe123"));
        }

        [Fact]
        public void CalculateInterest_ShouldReturnCorrectAmount()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var interest = account.CalculateInterest(0.05);
            Assert.Equal(5, interest);
        }

        [Fact]
        public void CalculateInterest_WithNegativeRate_ShouldThrowException()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            Assert.Throws<ArgumentException>(() => account.CalculateInterest(-0.05));
        }

        [Fact]
        public void CalculateInterest_WithZeroRate_ShouldReturnZero()
        {
            var account = new BankAccount("12345", 100, "John Doe", "Savings", DateTime.Now, "jdoe", "jdoe123");
            var interest = account.CalculateInterest(0);
            Assert.Equal(0, interest);
        }
    }
}
