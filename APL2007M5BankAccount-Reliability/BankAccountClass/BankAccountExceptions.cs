using System;

namespace BankAccountApp
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    public class TransferLimitExceededException : Exception
    {
        public TransferLimitExceededException(string message) : base(message) { }
    }
}