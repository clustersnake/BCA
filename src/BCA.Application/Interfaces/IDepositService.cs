using BCA.Domain.Common;

namespace BCA.Application.Interfaces;

public interface IDepositService
{
    Task<Result> Execute(Guid accountId, decimal amount);
}