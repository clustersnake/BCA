using NSubstitute;
using Xunit;
using BCA.Domain.Interfaces;
using BCA.UnitTests.Common;
using BCA.Domain.Enums;

namespace BCA.UnitTests.Application;

public class DepositServiceTests
{
    [Fact]
    public async Task Deposit_Should_Save_Changes_To_Repository()
    {
        // Arrange
        var repo = Substitute.For<IAccountRepository>();
        var account = TestDataFactory.CreateAccount();
        repo.GetByIdAsync(account.Id).Returns(account);
        
        // Aquí necesitaremos un servicio en Application (que no existe aún)
        // var service = new DepositService(repo);

        // Act
        // await service.Execute(account.Id, 100);

        // Assert
        // repo.Received(1).UpdateAsync(Arg.Is<Account>(a => a.Balance == 100));
        
        Assert.True(true); // Temporal para que compile el esqueleto
    }
}