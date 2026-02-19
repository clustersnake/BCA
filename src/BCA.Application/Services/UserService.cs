using BCA.Application.Common;
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

    public async Task<PagedResult<User>> GetUsersPagedAsync(int page, int pageSize)
    {
        // 1. Llamada al repositorio (Capa Infrastructure)
        var (items, totalCount) = await _userRepository.GetPagedAsync(page, pageSize);

        // 2. Aquí podrías aplicar lógica extra (ej. filtrar, mapear a DTOs, etc.)

        // 3. Empaquetado final para la capa de presentación
        return new PagedResult<User>
        {
            Items = items,
            PageNumber = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}