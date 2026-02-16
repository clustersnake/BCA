using BCA.Domain.Interfaces;
using BCA.Domain.Enums;
using BCA.Domain.Common;

namespace BCA.Application.Services;

public class WithdrawalService
{
    private readonly IAccountRepository _repository;

    public WithdrawalService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Execute(Guid accountId, decimal amount)
    {
        var account = await _repository.GetByIdAsync(accountId);
        
        if (account == null)
            return Result.Failure("La cuenta no existe.");

        try 
        {
            // La entidad Account ya tiene la lógica de lanzar 
            // InvalidOperationException si no hay saldo.
            account.AddTransaction(TransactionType.Withdrawal, amount);
            
            await _repository.UpdateAsync(account);
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            // Capturamos el error de negocio y lo devolvemos como un Result.Failure
            return Result.Failure(ex.Message);
        }
        catch (Exception)
        {
            return Result.Failure("Ocurrió un error inesperado al procesar el retiro.");
        }
    }
}