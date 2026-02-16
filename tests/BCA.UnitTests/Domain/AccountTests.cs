using BCA.UnitTests.Common;

namespace BCA.UnitTests.Domain;

public class AccountTests
{
    [Fact]
    public void Account_ShouldInitialize_WithZeroBalance()
    {
        // Arrange
        var account = TestDataFactory.CreateAccount();

        // Assert
        Assert.Equal(0, account.Balance);
    }
}