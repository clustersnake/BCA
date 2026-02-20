using NSubstitute;
using BCA.Domain.Interfaces;
using BCA.Application.Services;
using BCA.Domain.Entities;
using BCA.Application.Common;
using BCA.Application.DTOs;

namespace BCA.UnitTests.Application;

public class UserServiceTests
{

    [Fact]
    public async Task GetUsersPagedAsync_ShouldReturnPagedUserResponse()
    {
        // 1. Arrange
        var repo = Substitute.For<IUserRepository>();

        // Creamos un usuario de dominio para el simulacro
        var fakeUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Alan",
            LastName = "Turing",
            Email = "alan@turing.com",
            Role = new Role { Name = "Admin" },
            IsActive = true
        };

        var fakeUsersList = new List<User> { fakeUser };

        // Configuramos el repo para que devuelva la Tupla (Lista de Entidades, Total)
        repo.GetPagedAsync(1, 10).Returns(Task.FromResult(((IEnumerable<User>)fakeUsersList, 1)));

        var service = new UserService(repo);

        // 2. Act
        var result = await service.GetUsersPagedAsync(1, 10);

        // 3. Assert
        Assert.NotNull(result);
        Assert.IsType<PagedResult<UserResponse>>(result); // Verificamos el nuevo tipo
        Assert.NotEmpty(result.Data); // Verificamos que se llame 'Data' y no 'Items'

        var firstUser = result.Data.First();
        Assert.Equal("Alan Turing", firstUser.FullName); // Verificamos el mapeo manual
        Assert.Equal("Admin", firstUser.RoleName);       // Verificamos el aplanamiento del rol
        Assert.Equal(1, result.TotalCount);
    }
}

