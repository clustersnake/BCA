using BCA.Domain.Entities;
using BCA.Domain.Enums;

namespace BCA.UnitTests.Common;

public static class TestDataFactory
{
    public static User CreateUser(string firstName = "Juan", string lastName = "Perez")
    {
        return new User 
        { 
            FirstName = firstName, 
            LastName = lastName, 
            Role = new Role { Name = "Client" },
            Email = $"{firstName.ToLower()}@test.com"
        };
    }

    public static Account CreateAccount(User? owner = null, decimal initialDeposit = 0)
    {
        var account = new Account 
        { 
            AccountNumber = "12345", 
            Owner = owner ?? CreateUser(), 
            ProductType = new Product { Name = "Savings", InterestRate = 0.1m } 
        };

        if (initialDeposit > 0)
        {
            account.AddTransaction(TransactionType.Deposit, initialDeposit);
        }

        return account;
    }
}