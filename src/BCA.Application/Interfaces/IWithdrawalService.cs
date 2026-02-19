using BCA.Domain.Common;

namespace BCA.Application.Interfaces;

public interface IWithdrawalService
{
    Task<Result> Execute(Guid accountId, decimal amount);
}