using BCA.Domain.Entities;
using BCA.Domain.Enums;

namespace BCA.UnitTests.Domain;

public class TransactionTests
{
    [Fact]
    public void Deposit_ShouldIncreaseAccountBalance()
    {
        // Arrange
        var account = CreateTestAccount();

        decimal depositAmount = 100;

        // Act
        account.AddTransaction(TransactionType.Deposit, depositAmount);

        // Assert
        Assert.Equal(depositAmount, account.Balance);

    }

    private Account CreateTestAccount()
    {
        var role = new Role { Name = "Client" };
        var user = new User { FirstName = "Test", LastName = "User", Role = role };
        var product = new Product { Name = "Savings", InterestRate = 0.1m };

        return new Account
        {
            AccountNumber = "12345",
            Owner = user,
            ProductType = product
        };
    }
}
