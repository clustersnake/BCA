using BCA.Domain.Common;

namespace BCA.Domain.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public required Role Role { get; set; }
    public bool IsActive { get; set; } = true;
}