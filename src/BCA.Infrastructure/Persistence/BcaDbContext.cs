using Microsoft.EntityFrameworkCore;
using BCA.Domain.Entities;

namespace BCA.Infrastructure.Persistence;

public class BcaDbContext : DbContext
{
    public BcaDbContext(DbContextOptions<BcaDbContext> options) : base(options) { }

    // Tablas (DbSets)
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de Account
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccountNumber).IsRequired().HasMaxLength(20);
            
            // Aquí configuramos que el Balance solo sea leído por EF
            entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
        });

        // Configuración de Transaction
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        });

        base.OnModelCreating(modelBuilder);
    }
}