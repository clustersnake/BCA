using BCA.Domain.Interfaces;
using BCA.Domain.Enums;
using BCA.Domain.Common;
using BCA.Application.Interfaces;

namespace BCA.Application.Services;

public class DepositService : IDepositService
{
    private readonly IAccountRepository _repository;

    public DepositService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Execute(Guid accountId, decimal amount)
    {
        // 1. Orquestación: Obtener el dato
        var account = await _repository.GetByIdAsync(accountId);

        if (account == null)
        {
            return Result.Failure("Cuenta no encontrada.");
        }

        try
        {

            // 2. Delegación: La lógica de negocio la hace el DOMINIO
            account.AddTransaction(TransactionType.Deposit, amount);

            // 3. Orquestación: Persistir el cambio
            await _repository.UpdateAsync(account);

            return Result.Success();
        }
        catch (Exception ex)
        {
            // Aquí podríamos loguear el error, etc.
            return Result.Failure(ex.Message);
        }
    }
}