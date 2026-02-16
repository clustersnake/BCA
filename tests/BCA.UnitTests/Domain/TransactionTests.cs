using BCA.Domain.Enums;
using BCA.UnitTests.Common;

namespace BCA.UnitTests.Domain;

public class TransactionTests
{
    [Fact]
    public void Deposit_ShouldIncreaseAccountBalance()
    {
        // Arrange
        var account = TestDataFactory.CreateAccount();

        decimal depositAmount = 100;

        // Act
        account.AddTransaction(TransactionType.Deposit, depositAmount);

        // Assert
        Assert.Equal(depositAmount, account.Balance);

    }

    [Fact]
    public void Withdeawal_ShouldFailIfInsufficientFunds()
    {
        // Arrange
        var account = TestDataFactory.CreateAccount(initialDeposit: 50);

        decimal withdrawalAmount = 100;

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => account.AddTransaction(TransactionType.Withdrawal, withdrawalAmount));

        Assert.Equal("Fondos insuficientes para realizar el retiro.", exception.Message);
    }


}
