using Bogus;
using BCA.Domain.Entities;
using BCA.Domain.Enums;
using BCA.Infrastructure.Persistence;

namespace BCA.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Seed(BcaDbContext context)
    {
        // 1. Verificar si ya existen datos
        if (context.Users.Any()) return;

        // Configurar Bogus en español (opcional)
        var faker = new Faker("es");

        // 2. Crear Roles y Productos base
        var clientRole = new Role { Name = "Client" };
        var savingsProduct = new Product { Name = "Savings Account", InterestRate = 0.05m };

        context.Roles.Add(clientRole);
        context.Products.Add(savingsProduct);
        context.SaveChanges();

        // 3. Generar 100 Usuarios
        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(1)) // Fecha en el último año
            .RuleFor(u => u.PasswordHash, f => "BCADefaultHash123!") // Hash simulado
            .RuleFor(u => u.Role, f => clientRole);

        var users = userFaker.Generate(100);
        context.Users.AddRange(users);
        context.SaveChanges();

        // 4. Generar 100 Cuentas (una para cada usuario)
        var accountFaker = new Faker<Account>()
            .RuleFor(a => a.AccountNumber, f => f.Finance.Account(10))
            .RuleFor(a => a.ProductType, f => savingsProduct);

        foreach (var user in users)
        {
            var account = accountFaker.Generate();
            account.Owner = user;
            // La cuenta se creó un poco después que el usuario
            account.CreatedAt = faker.Date.Between(user.CreatedAt, DateTime.UtcNow);

            // 5. Agregar un depósito inicial aleatorio entre 100 y 5000
            decimal initialAmount = faker.Random.Decimal(100, 5000);
            account.AddTransaction(TransactionType.Deposit, initialAmount);

            // Si quieres que las transacciones también tengan fechas realistas:
            var lastTx = account.Transactions.Last();
            lastTx.CreatedAt = account.CreatedAt; // El depósito inicial fue al abrir la cuenta

            context.Accounts.Add(account);
        }

        context.SaveChanges();
    }
}