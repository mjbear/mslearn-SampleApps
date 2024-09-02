namespace System.Numbers.UnitTests;

public class PrimeServiceTests
{
    private readonly PrimeService _primeService;

    public PrimeServiceTests()
    {
        _primeService = new PrimeService();
    }

    [Fact]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        var result = _primeService.IsPrime(1);

        Assert.False(result, "1 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIs4_ReturnFalse()
    {
        var result = _primeService.IsPrime(4);

        Assert.False(result, "4 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIs5_ReturnTrue()
    {
        var result = _primeService.IsPrime(5);

        Assert.True(result, "5 should be prime");
    }

    [Fact]
    public void IsPrime_InputIsNegative_ReturnFalse()
    {
        var result = _primeService.IsPrime(-1);
        Assert.False(result, "-1 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIsZero_ReturnFalse()
    {
        var result = _primeService.IsPrime(0);
        Assert.False(result, "0 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIsLargePrime_ReturnTrue()
    {
        var result = _primeService.IsPrime(7919); // 7919 is a prime number
        Assert.True(result, "7919 should be prime");
    }

    [Fact]
    public void IsPrime_InputIsLargeNonPrime_ReturnFalse()
    {
        var result = _primeService.IsPrime(8000); // 8000 is not a prime number
        Assert.False(result, "8000 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIsProductOfTwoPrimes_ReturnFalse()
    {
        var result = _primeService.IsPrime(15);
        Assert.False(result, "15 should not be prime");
    }
}