using NSubstitute;
using BCA.Domain.Interfaces;
using BCA.Application.Services;
using BCA.Domain.Entities;

namespace BCA.UnitTests.Application;

public class UserServiceTests
{

    [Fact]
    public async Task GetUsersPaged_ShouldReturnCorrectPagedResult()
    {
        // 1. Arrange
        var repo = Substitute.For<IUserRepository>();
        var fakeUsers = new List<User> { new User { FirstName = "Gemini", LastName = "Santos", Role = new Role { Name = "Admin"} } };

        // Configuramos el retorno de la Tupla (Items, TotalCount)
        repo.GetPagedAsync(1, 10).Returns(Task.FromResult(((IEnumerable<User>)fakeUsers, 1)));

        var service = new UserService(repo);

        // 2. Act
        var result = await service.GetUsersPagedAsync(1, 10);

        // 3. Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Gemini", result.Items.First().FirstName);
    }
}

