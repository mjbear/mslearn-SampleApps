namespace BankAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> accounts = new List<BankAccount>();

            // Create bank accounts with random balances
            int numberOfAccounts = 20;
            int createdAccounts = 0;
            while (createdAccounts < numberOfAccounts)
            {
                try
                {
                    double initialBalance = GenerateRandomBalance(10, 50000);
                    string accountHolderName = GenerateRandomAccountHolder();
                    string accountType = GenerateRandomAccountType();
                    DateTime dateOpened = GenerateRandomDateOpened();
                    BankAccount account = new BankAccount($"Account {createdAccounts + 1}", initialBalance, accountHolderName, accountType, dateOpened);
                    accounts.Add(account);
                    createdAccounts++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Account creation failed: {ex.Message}");
                }
            }

            // Simulate 100 transactions for each account
            foreach (BankAccount account in accounts)
            {
                for (int i = 0; i < 100; i++)
                {
                    double transactionAmount = GenerateRandomBalance(-500, 500);
                    try
                    {
                        if (transactionAmount >= 0)
                        {
                            account.Credit(transactionAmount);
                            Console.WriteLine($"Credit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                        else
                        {
                            account.Debit(-transactionAmount);
                            Console.WriteLine($"Debit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Transaction failed: {ex.Message}");
                    }
                }

                Console.WriteLine($"Account: {account.AccountNumber}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
            }

            // Simulate transfers between accounts
            foreach (BankAccount fromAccount in accounts)
            {
                foreach (BankAccount toAccount in accounts)
                {
                    if (fromAccount != toAccount)
                    {
                        try
                        {
                            double transferAmount = GenerateRandomBalance(0, fromAccount.Balance);
                            fromAccount.Transfer(toAccount, transferAmount);
                            Console.WriteLine($"Transfer: {transferAmount.ToString("C")} from {fromAccount.AccountNumber} ({fromAccount.AccountHolderName}, {fromAccount.AccountType}) to {toAccount.AccountNumber} ({toAccount.AccountHolderName}, {toAccount.AccountType})");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed: {ex.Message}"); 
                        }
                    }
                }
            }
        }
        
        public class RandomGenerator : IRandomGenerator
        {
            private static readonly Random random = new Random();
            private const int YearsRange = 10;
        
            public double GenerateRandomBalance(double min, double max)
            {
                double balance = random.NextDouble() * (max - min) + min;
                return Math.Round(balance, 2);
            }
        
            public string GenerateRandomAccountHolder()
            {
                string[] accountHolderNames = 
                {
                    "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois", "Liam Johnson", 
                    "Emma Martinez", "Noah Lee", "Olivia Kim", "William Chen", "Ava Wang", 
                    "James Brown", "Isabella Nguyen", "Benjamin Wilson", "Mia Li", "Lucas Anderson", 
                    "Charlotte Liu", "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim"
                };
        
                return accountHolderNames[random.Next(accountHolderNames.Length)];
            }
        
            public string GenerateRandomAccountType()
            {
                string[] accountTypes = { "Savings", "Checking", "Money Market", "Certificate of Deposit", "Retirement" };
                return accountTypes[random.Next(accountTypes.Length)];
            }
        
            public DateTime GenerateRandomDateOpened()
            {
                DateTime startDate = new DateTime(DateTime.Today.Year - YearsRange, 1, 1);
                int daysRange = (DateTime.Today - startDate).Days;
                DateTime randomDate = startDate.AddDays(random.Next(daysRange));
        
                // Ensure the date is not in the future
                if (randomDate >= DateTime.Today)
                {
                    randomDate = randomDate.AddDays(-1);
                }
        
                return randomDate;
            }
        }
        
        static Random random = new Random();
        const int YearsRange = 10;
        
        /// <summary>
        /// Generates a random balance within the specified range.
        /// </summary>
        /// <param name="min">The minimum balance.</param>
        /// <param name="max">The maximum balance.</param>
        /// <returns>A random balance.</returns>
        static double GenerateRandomBalance(double min, double max)
        {
            double balance = random.NextDouble() * (max - min) + min;
            return Math.Round(balance, 2);
        }
        
        /// <summary>
        /// Generates a random account holder name.
        /// </summary>
        /// <returns>A random account holder name.</returns>
        static string GenerateRandomAccountHolder()
        {
            string[] accountHolderNames = 
            {
                "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois", "Liam Johnson", 
                "Emma Martinez", "Noah Lee", "Olivia Kim", "William Chen", "Ava Wang", 
                "James Brown", "Isabella Nguyen", "Benjamin Wilson", "Mia Li", "Lucas Anderson", 
                "Charlotte Liu", "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim"
            };
        
            return accountHolderNames[random.Next(accountHolderNames.Length)];
        }
        
        /// <summary>
        /// Generates a random account type.
        /// </summary>
        /// <returns>A random account type.</returns>
        static string GenerateRandomAccountType()
        {
            string[] accountTypes = { "Savings", "Checking", "Money Market", "Certificate of Deposit", "Retirement" };
            return accountTypes[random.Next(accountTypes.Length)];
        }
        
        /// <summary>
        /// Generates a random date within the last 10 years.
        /// </summary>
        /// <returns>A random date.</returns>
        static DateTime GenerateRandomDateOpened()
        {
            DateTime startDate = new DateTime(DateTime.Today.Year - YearsRange, 1, 1);
            int daysRange = (DateTime.Today - startDate).Days;
            DateTime randomDate = startDate.AddDays(random.Next(daysRange));
        
            // Ensure the date is not in the future
            if (randomDate >= DateTime.Today)
            {
                randomDate = randomDate.AddDays(-1);
            }
        
            return randomDate;
        }
    }
    
    public interface IRandomGenerator
    {
        double GenerateRandomBalance(double min, double max);
        string GenerateRandomAccountHolder();
        string GenerateRandomAccountType();
        DateTime GenerateRandomDateOpened();
    }
}