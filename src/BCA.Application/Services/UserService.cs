using BCA.Application.Common;
using BCA.Application.DTOs;
using BCA.Application.Interfaces;
using BCA.Domain.Entities;
using BCA.Domain.Interfaces;

namespace BCA.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserResponse>> GetUsersPagedAsync(int page, int pageSize)
    {
        // 1. Llamada al repositorio (Capa Infrastructure)
        var (users, totalCount) = await _userRepository.GetPagedAsync(page, pageSize);

        // 2. Aquí podrías aplicar lógica extra (ej. filtrar, mapear a DTOs, etc.)
        var userDtos = users.Select(u => new UserResponse
        {
            Id = u.Id,
            FullName = $"{u.FirstName} {u.LastName}",
            Email = u.Email,
            RoleName = u.Role.Name ?? "No Role",
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        });

        // 3. Empaquetado final para la capa de presentación
        return new PagedResult<UserResponse>
        {
            Data = userDtos,
            PageNumber = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}