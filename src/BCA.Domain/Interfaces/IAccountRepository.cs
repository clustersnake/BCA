using BCA.Domain.Entities;

namespace BCA.Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task UpdateAsync(Account account);
    Task AddAsync(Account account);
}