using BCA.Domain.Interfaces;
using BCA.Domain.Enums;

namespace BCA.Application.Services;

public class DepositService
{
    private readonly IAccountRepository _repository;

    public DepositService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(Guid accountId, decimal amount)
    {
        // 1. Orquestación: Obtener el dato
        var account = await _repository.GetByIdAsync(accountId) ?? throw new Exception("Cuenta no encontrada");

        // 2. Delegación: La lógica de negocio la hace el DOMINIO
        account.AddTransaction(TransactionType.Deposit, amount);

        // 3. Orquestación: Persistir el cambio
        await _repository.UpdateAsync(account);
    }
}