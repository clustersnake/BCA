using BCA.Domain.Entities;
using BCA.Domain.Interfaces;
using BCA.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BCA.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BcaDbContext _context;

    public AccountRepository(BcaDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts
            .Include(a => a.Owner)      // Carga el usuario
            .Include(a => a.ProductType) // Carga el producto
            .Include(a => a.Transactions) // Carga historial
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}