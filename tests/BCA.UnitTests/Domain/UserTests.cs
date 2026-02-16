using BCA.Domain.Entities;

namespace BCA.UnitTests.Domain;

public class UserTests
{
    [Fact]
    public void User_ShouldHavePermission_WhenRoleHasIt()
    {
        // Arrange
        var permission = new Permission { Name = "VIEW_AUDIT_LOGS" };
        var adminRole = new Role { Name = "Auditor" };
        adminRole.Permissions.Add(permission);

        // Act
        var user = new User
        {
            FirstName = "Julian",
            LastName = "da Silva",
            Role = adminRole
        };

        // Assert
        Assert.Contains(user.Role.Permissions, p => p.Name == "VIEW_AUDIT_LOGS");

    }
}