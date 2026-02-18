using BCA.Domain.Enums;
using BCA.Domain.Common;

namespace BCA.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}