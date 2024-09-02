namespace System.Numbers.UnitTests;

public class PrimeServiceTests
{
    private readonly PrimeService _primeService;

    public PrimeServiceTests()
    {
        _primeService = new PrimeService();
    }

    [Fact]
    public void IsPrime_InputIs1_ReturnsFalse()
    {
        var result = _primeService.IsPrime(1);
        Assert.False(result, "1 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIs2_ReturnsTrue()
    {
        var result = _primeService.IsPrime(2);
        Assert.True(result, "2 should be prime");
    }

    [Fact]
    public void IsPrime_InputIs3_ReturnsTrue()
    {
        var result = _primeService.IsPrime(3);
        Assert.True(result, "3 should be prime");
    }

    [Fact]
    public void IsPrime_InputIs4_ReturnsFalse()
    {
        var result = _primeService.IsPrime(4);
        Assert.False(result, "4 should not be prime");
    }

    [Fact]
    public void IsPrime_InputIs17_ReturnsTrue()
    {
        var result = _primeService.IsPrime(17);
        Assert.True(result, "17 should be prime");
    }

    [Fact]
    public void IsPrime_InputIs18_ReturnsFalse()
    {
        var result = _primeService.IsPrime(18);
        Assert.False(result, "18 should not be prime");
    }
}