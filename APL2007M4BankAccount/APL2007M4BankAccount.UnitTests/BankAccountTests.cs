// using System.BankAccount; // Add the correct namespace import
using BankAccountApp;

namespace System.BankAccountNS.UnitTests
{
    public class BankAccountTests
    {
        [Fact]
        public void CalculateInterest_ShouldHandleNegativeInterestRate()
        {
            // Arrange
            var account = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = -0.05;

            // Act
            var interest = account.CalculateInterest(interestRate);

            // Assert
            Assert.Equal(-10.0, interest);
        }

        [Fact]
        public void CalculateInterest_ShouldReturnCorrectInterest()
        {
            // Arrange
            var account = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = 0.05;

            // Act
            var interest = account.CalculateInterest(interestRate);

            // Assert
            Assert.Equal(10.0, interest);
        }

        [Fact]
        public void CalculateInterest_ShouldReturnZeroForZeroBalance()
        {
            // Arrange
            var account = new BankAccount("12345", 0.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = 0.05;

            // Act
            var interest = account.CalculateInterest(interestRate);

            // Assert
            Assert.Equal(0.0, interest);
        }

        [Fact]
        public void Credit_ShouldHandleMaxDecimalValue()
        {
            // Arrange
            var account = new BankAccount("12345", 0.0, "John Doe", "Savings", DateTime.Now);
            var creditAmount = decimal.MaxValue;

            // Act
            account.Credit((double)creditAmount);

            // Assert
            Assert.Equal((double)creditAmount, account.GetBalance());
        }

        [Fact]
        public void Credit_ShouldIncreaseBalance()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var creditAmount = 50.0;

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.Equal(initialBalance + creditAmount, account.GetBalance());
        }

        [Fact]
        public void Credit_ShouldHandleMultipleCreditsCorrectly()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var creditAmount1 = 50.0;
            var creditAmount2 = 75.0;

            // Act
            account.Credit(creditAmount1);
            account.Credit(creditAmount2);

            // Assert
            Assert.Equal(initialBalance + creditAmount1 + creditAmount2, account.GetBalance());
        }

        /*
        [Fact]
        public void Credit_ShouldNotAcceptNegativeAmount()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var creditAmount = -50.0;

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.Equal(initialBalance, account.GetBalance()); // Assuming negative credits are ignored
        }
        */

        [Fact]
        public void Credit_ShouldNotAcceptZeroAmount()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var creditAmount = 0.0;

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.Equal(initialBalance, account.GetBalance()); // Assuming zero credits are ignored
        }

        /*
        [Fact]
        public void Credit_ShouldThrowException_WhenAmountIsInfinity()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var creditAmount = double.PositiveInfinity;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Credit(creditAmount));
            Assert.Equal("Amount must be a finite number.", exception.Message);
        }

        [Fact]
        public void Credit_ShouldThrowException_WhenAmountIsNaN()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var creditAmount = double.NaN;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Credit(creditAmount));
            Assert.Equal("Amount must be a valid number.", exception.Message);
        }

        [Fact]
        public void Credit_ShouldThrowException_WhenAmountIsNegative()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var creditAmount = -50.0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Credit(creditAmount));
            Assert.Equal("Amount must be positive.", exception.Message);
        }
        */

        [Fact]
        public void Debit_ShouldDecreaseBalance()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var debitAmount = 50.0;

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.Equal(initialBalance - debitAmount, account.GetBalance());
        }

        [Fact]
        public void Debit_ShouldHandleMaxDecimalValue()
        {
            // Arrange
            var account = new BankAccount("12345", (double)decimal.MaxValue, "John Doe", "Savings", DateTime.Now);
            var debitAmount = decimal.MaxValue;

            // Act
            account.Debit((double)debitAmount);

            // Assert
            Assert.Equal(0.0, account.GetBalance());
        }

        [Fact]
        public void Debit_ShouldHandleMultipleDebitsCorrectly()
        {
            // Arrange
            var account = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var debitAmount1 = 50.0;
            var debitAmount2 = 75.0;

            // Act
            account.Debit(debitAmount1);
            account.Debit(debitAmount2);

            // Assert
            Assert.Equal(initialBalance - debitAmount1 - debitAmount2, account.GetBalance());
        }

        [Fact]
        public void Debit_ShouldNotAcceptNegativeAmount()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var debitAmount = -50.0;

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.Equal(initialBalance, account.GetBalance()); // Assuming negative debits are ignored
        }

        [Fact]
        public void Debit_ShouldNotAcceptZeroAmount()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var initialBalance = account.GetBalance();
            var debitAmount = 0.0;

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.Equal(initialBalance, account.GetBalance()); // Assuming zero debits are ignored
        }

        /*
        [Fact]
        public void Debit_ShouldThrowException_WhenAmountIsInfinity()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var debitAmount = double.PositiveInfinity;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Debit(debitAmount));
            Assert.Equal("Amount must be a finite number.", exception.Message);
        }

        [Fact]
        public void Debit_ShouldThrowException_WhenAmountIsNaN()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var debitAmount = double.NaN;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Debit(debitAmount));
            Assert.Equal("Amount must be a valid number.", exception.Message);
        }

        [Fact]
        public void Debit_ShouldThrowException_WhenAmountIsNegative()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var debitAmount = -50.0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Debit(debitAmount));
            Assert.Equal("Amount must be positive.", exception.Message);
        }
        */

        [Fact]
        public void Debit_ShouldThrowException_WhenInsufficientBalance()
        {
            // Arrange
            var account = new BankAccount("12345", 100.0, "John Doe", "Savings", DateTime.Now);
            var debitAmount = 150.0;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => account.Debit(debitAmount));
            Assert.Equal("Insufficient balance for debit.", exception.Message);
        }

        /*
        [Fact]
        public void Debit_ShouldNotUnderflowBalance()
        {
            // Arrange
            var account = new BankAccount("12345", 0.0, "John Doe", "Savings", DateTime.Now);
            var debitAmount = 1.0;

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.Equal(0.0, account.GetBalance()); // Assuming balance does not go negative
        }
        */

        [Fact]
        public void PrintStatement_ShouldOutputCorrectFormat()
        {
            // Arrange
            var account = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            account.PrintStatement();

            // Assert
            var expectedOutput = $"Account Number: 12345, Balance: 200{Environment.NewLine}";
            Assert.Equal(expectedOutput, sw.ToString());
        }

        [Fact]
        public void Transfer_ShouldHandleMaxDecimalValue()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", (double)decimal.MaxValue, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 0.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = decimal.MaxValue;

            // Act
            sourceAccount.Transfer(targetAccount, (double)transferAmount);

            // Assert
            Assert.Equal(0.0, sourceAccount.GetBalance());
            Assert.Equal((double)transferAmount, targetAccount.GetBalance());
        }

        [Fact]
        public void Transfer_ShouldIncreaseBalanceOfTargetAccount()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = 50.0;

            // Act
            sourceAccount.Transfer(targetAccount, transferAmount);

            // Assert
            Assert.Equal(150.0, targetAccount.GetBalance());
        }

        [Fact]
        public void Transfer_ShouldNotAcceptZeroAmount()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = 0.0;

            // Act
            sourceAccount.Transfer(targetAccount, transferAmount);

            // Assert
            Assert.Equal(200.0, sourceAccount.GetBalance()); // Assuming zero transfers are ignored
            Assert.Equal(100.0, targetAccount.GetBalance());
        }

        [Fact]
        public void Transfer_ShouldThrowException_WhenAmountIsInfinity()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = double.PositiveInfinity;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => sourceAccount.Transfer(targetAccount, transferAmount));
            Assert.Equal("Amount must be a finite number.", exception.Message);
        }

        [Fact]
        public void Transfer_ShouldThrowException_WhenAmountIsNaN()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = double.NaN;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => sourceAccount.Transfer(targetAccount, transferAmount));
            Assert.Equal("Amount must be a valid number.", exception.Message);
        }

        /*
        [Fact]
        public void Transfer_ShouldThrowException_WhenAmountIsNegative()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 200.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = -50.0;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => sourceAccount.Transfer(targetAccount, transferAmount));
            Assert.Equal("Amount must be positive.", exception.Message);
        }
        */

        [Fact]
        public void Transfer_ShouldThrowException_WhenInsufficientBalance()
        {
            // Arrange
            var sourceAccount = new BankAccount("12345", 50.0, "John Doe", "Savings", DateTime.Now);
            var targetAccount = new BankAccount("67890", 100.0, "Jane Doe", "Savings", DateTime.Now);
            var transferAmount = 100.0;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => sourceAccount.Transfer(targetAccount, transferAmount));
            Assert.Equal("Insufficient balance for transfer.", exception.Message);
        }
    }
}