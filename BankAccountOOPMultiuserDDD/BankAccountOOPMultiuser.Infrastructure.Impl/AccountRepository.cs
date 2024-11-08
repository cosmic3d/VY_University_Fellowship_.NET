using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Datos;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyFirstDataBaseContext _dbContext = new();

        public List<AccountEntity>? GetAllAccounts()
        {
            List<AccountEntity>? accounts = _dbContext.Accounts.Include(c => c.Movements).ToList();
            if (accounts.Count == 0) return null;
            return accounts;
        }
        public AccountEntity AddAccountToRepository(AccountEntity account)
        { 
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }

        public AccountEntity? GetAccountFromRepository(string accountIBAN) =>
            _dbContext.Accounts.Include(c => c.Movements).Where(acc => acc.Iban.ToLower() == accountIBAN.ToLower()).FirstOrDefault();

        public AccountEntity? AddMovementToAccount(AccountEntity account, Tuple<DateTime, Decimal> movement)
        {
            if (account is null || movement is null) return null;
            Movement newMovement = new()
            {
                Date = movement.Item1,
                Value = movement.Item2
            };
            account.Movements.Add(newMovement);
            _dbContext.SaveChanges();
            return account;
        }

        public static List<Tuple<DateTime, Decimal>> MovementCollectionToList(AccountEntity account)
        {
            List<Tuple<DateTime, Decimal>> movementList = new();
            foreach (var movement in account.Movements)
            {
                movementList.Add(new(movement.Date, movement.Value));
            }
            return movementList;

        }

        public void DeleteAccountFromRepository(AccountEntity account)
        {
            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }
    }

}