using Microsoft.Extensions.Configuration;
using NSubstitute;
using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Xunit;

namespace Library.UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
    private readonly ILoanRepository _mockLoanRepository;
    private readonly JsonLoanRepository _jsonLoanRepository;
    private readonly IConfiguration _configuration;
    private readonly JsonData _jsonData;

    public GetLoanTest()
    {
        // Create a mock loan repository
        _mockLoanRepository = Substitute.For<ILoanRepository>();

        // Build configuration
        _configuration = new ConfigurationBuilder().Build();

        // Instantiate JsonData with the configuration
        _jsonData = new JsonData(_configuration);

        // Instantiate JsonLoanRepository with the JsonData object
        _jsonLoanRepository = new JsonLoanRepository(_jsonData);
    }

    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan if found")]
    public async Task GetLoan_ReturnsLoanIfFound()
    {
        // Arrange
        var loanId = 1; // Assuming this ID exists in Loans.json
        var expectedLoan = new Loan
        {
            Id = loanId,
            BookItemId = 101,
            PatronId = 201,
            LoanDate = DateTime.Now.AddDays(-10),
            DueDate = DateTime.Now.AddDays(10)
        };

        _mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.NotNull(actualLoan);
        Assert.Equal(expectedLoan.Id, actualLoan?.Id);
    }

    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns null if not found")]
    public async Task GetLoan_ReturnsNullIfNotFound()
    {
        // Arrange
        var loanId = 999; // Assuming this ID does not exist in Loans.json

        _mockLoanRepository.GetLoan(loanId).Returns((Loan)null);

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.Null(actualLoan);
    }

    // Additional test methods can go here
}