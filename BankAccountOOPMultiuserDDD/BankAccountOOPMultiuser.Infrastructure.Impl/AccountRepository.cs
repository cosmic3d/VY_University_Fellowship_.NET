using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Datos;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyFirstDataBaseContext _dbContext = new();

        public List<Account>? GetAllAccounts()
        {
            List<Account>? accounts = _dbContext.Accounts.ToList();
            if (accounts.Count == 0) return null;
            return accounts;
        }
        public Account AddAccountToRepository(Account account)
        { 
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }

        public Account? GetAccountFromRepository(string accountIBAN) =>
            _dbContext.Accounts.Where(acc => acc.Iban.ToLower() == accountIBAN.ToLower()).FirstOrDefault();

        public Account? AddMovementToAccount(Account account, Tuple<DateTime, Decimal> movement)
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

        public static List<Tuple<DateTime, Decimal>> MovementCollectionToList(Account account)
        {
            List<Tuple<DateTime, Decimal>> movementList = new();
            foreach (var movement in account.Movements)
            {
                movementList.Add(new(movement.Date, movement.Value));
            }
            return movementList;

        }
    }

}