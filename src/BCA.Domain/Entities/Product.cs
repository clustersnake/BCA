using BCA.Domain.Common;
namespace BCA.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal InterestRate { get; set; }
}