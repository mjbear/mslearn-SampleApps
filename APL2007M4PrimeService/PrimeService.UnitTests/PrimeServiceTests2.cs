using Xunit;

namespace System.Numbers.Tests
{
    public class PrimeServiceTests
    {
        [Fact]
        public void IsPrime_ReturnsFalse_ForNegativeNumbers()
        {
            // Arrange
            var primeService = new PrimeService();

            // Act
            var result = primeService.IsPrime(-1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPrime_ReturnsFalse_ForZero()
        {
            // Arrange
            var primeService = new PrimeService();

            // Act
            var result = primeService.IsPrime(0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPrime_ReturnsFalse_ForOne()
        {
            // Arrange
            var primeService = new PrimeService();

            // Act
            var result = primeService.IsPrime(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPrime_ReturnsTrue_ForPrimeNumbers()
        {
            // Arrange
            var primeService = new PrimeService();

            // Act
            var result = primeService.IsPrime(7);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPrime_ReturnsFalse_ForNonPrimeNumbers()
        {
            // Arrange
            var primeService = new PrimeService();

            // Act
            var result = primeService.IsPrime(10);

            // Assert
            Assert.False(result);
        }
    }
}