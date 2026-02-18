using BCA.Domain.Common;
namespace BCA.Domain.Entities;
public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<Permission> Permissions { get; set; } = [];
}