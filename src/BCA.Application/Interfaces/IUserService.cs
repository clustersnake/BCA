using BCA.Application.Common;
using BCA.Application.DTOs;

namespace BCA.Application.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserResponse>> GetUsersPagedAsync(int page, int pageSize);
}