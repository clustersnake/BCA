using BCA.Domain.Enums;

namespace BCA.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string AccountNumber { get; set; }
    public decimal Balance { get; private set; } = 0;
    public required User Owner { get; set; }
    public required Product ProductType { get; set; }

    // Lista de transacciones asociadas
    public List<Transaction> Transactions { get; private set; } = [];

    public void AddTransaction(TransactionType type, decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("El monto debe ser positivo");

        // Lógica de validación para retiros
        if (type == TransactionType.Withdrawal && amount > Balance)
        {
            throw new InvalidOperationException("Fondos insuficientes para realizar el retiro.");
        }

        var transaction = new Transaction
        {
            AccountId = this.Id,
            Amount = amount,
            Type = type,
            Date = DateTime.UtcNow
        };

        Transactions.Add(transaction);

        // Lógica de afectación de balance
        if (type == TransactionType.Deposit)
        {
            Balance += amount;
        }
        else if (type == TransactionType.Withdrawal)
        {
            // Aquí podríamos agregar lógica de "si tiene fondos suficientes" más adelante
            Balance -= amount;
        }
    }
}