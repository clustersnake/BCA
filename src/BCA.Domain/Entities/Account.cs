namespace BCA.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string AccountNumber { get; set; }
    public required User Owner { get; set; }
    public required Product ProductType { get; set; }
    public decimal Balance { get; private set; } = 0;
}