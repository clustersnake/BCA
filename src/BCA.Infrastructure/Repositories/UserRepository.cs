using BCA.Domain.Entities;
using BCA.Domain.Interfaces;
using BCA.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BCA.Application.Common;

namespace BCA.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BcaDbContext _context;

    public UserRepository(BcaDbContext context) => _context = context;

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.Include(u => u.Role).ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<(IEnumerable<User> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Users.Include(u => u.Role).AsNoTracking();

        var totalCount = await query.CountAsync(); // Obtenemos el total real en la DB
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount); // Devolvemos ambos valores
    }
}