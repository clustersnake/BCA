using BCA.Domain.Common;
namespace BCA.Domain.Entities;

public class Permission : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }

}
