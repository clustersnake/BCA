using BCA.Application.Common;
using BCA.Domain.Entities;

namespace BCA.Application.Interfaces;

public interface IUserService
{
    Task<PagedResult<User>> GetUsersPagedAsync(int page, int pageSize);
}