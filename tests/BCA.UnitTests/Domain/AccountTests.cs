using Xunit;
using BCA.Domain.Entities;

namespace BCA.UnitTests.Domain;

public class AccountTests
{
    [Fact]
    public void Account_ShouldInitialize_WithZeroBalance()
    {
        // Arrange

        var product = new Product
        {
            Name = "Savings Account",
            InterestRate = 0.01m
        };

        var user = new User
        {
            FirstName = "Alice",
            LastName = "Smith",
            Role = new Role
            {
                Name = "Customer"
            }
        };

        var account = new Account
        {
            AccountNumber = "1234567890",
            ProductType = product,
            Owner = user
        };

        // Assert
        Assert.Equal(0, account.Balance);
    }
}