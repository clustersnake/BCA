using NSubstitute;
using Xunit;
using BCA.Domain.Interfaces;
using BCA.UnitTests.Common;
using BCA.Application.Services;

namespace BCA.UnitTests.Application;

public class WithdrawalServiceTests
{
    [Fact]
    public async Task Withdrawal_Should_Decrease_Balance_And_Call_Update()
    {
        // Arrange
        var repo = Substitute.For<IAccountRepository>();
        var account = TestDataFactory.CreateAccount(initialDeposit: 500);
        repo.GetByIdAsync(account.Id).Returns(account);
        
        var service = new WithdrawalService(repo);
        decimal amount = 200;

        // Act
        var result = await service.Execute(account.Id, amount);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(300, account.Balance);
        await repo.Received(1).UpdateAsync(account);
    }

    [Fact]
    public async Task Withdrawal_Should_Fail_If_Insufficient_Funds()
    {
        // Arrange
        var repo = Substitute.For<IAccountRepository>();
        var account = TestDataFactory.CreateAccount(initialDeposit: 100);
        repo.GetByIdAsync(account.Id).Returns(account);
        
        var service = new WithdrawalService(repo);

        // Act
        var result = await service.Execute(account.Id, 500);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Fondos insuficientes para realizar el retiro.", result.Error);
    }
}