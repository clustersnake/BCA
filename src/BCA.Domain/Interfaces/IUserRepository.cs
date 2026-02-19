using BCA.Domain.Entities;

namespace BCA.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
     Task<IEnumerable<User>> GetPagedAsync(int pageNumber, int pageSize);
}