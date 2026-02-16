using NSubstitute;
using Xunit;
using BCA.Domain.Interfaces;
using BCA.UnitTests.Common;
using BCA.Application.Services;
using BCA.Domain.Entities;

namespace BCA.UnitTests.Application;

public class DepositServiceTests
{
    [Fact]
    public async Task Deposit_Should_Update_Account_And_Call_Repository_Once()
    {
        // 1. Arrange
        var repo = Substitute.For<IAccountRepository>();
        var account = TestDataFactory.CreateAccount(); // Balance inicial 0
        repo.GetByIdAsync(account.Id).Returns(account);
        
        var service = new DepositService(repo);
        decimal depositAmount = 150;

        // 2. Act
        var result = await service.Execute(account.Id, depositAmount);

        // 3. Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(150, account.Balance);
        
        // Verificamos que se llamó al método Update del repositorio exactamente una vez
        await repo.Received(1).UpdateAsync(Arg.Is<Account>(a => a.Id == account.Id));
    }

    [Fact]
    public async Task Deposit_Should_Return_Failure_If_Account_Does_Not_Exist()
    {
        // Arrange
        var repo = Substitute.For<IAccountRepository>();
        repo.GetByIdAsync(Arg.Any<Guid>()).Returns((Account?)null);
        
        var service = new DepositService(repo);

        // Act
        var result = await service.Execute(Guid.NewGuid(), 100);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Cuenta no encontrada.", result.Error);
    }
}