namespace BCA.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal InterestRate { get; set; }
}