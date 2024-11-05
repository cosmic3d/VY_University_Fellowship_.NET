using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Datos;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyFirstDataBaseContext _dbContext = new();
        public Account AddAccountToRepository(Account account)
        { 
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }

        public Account? GetAccountFromRepository(string accountIBAN) =>
            _dbContext.Accounts.Where(acc => acc.Iban.ToLower() == accountIBAN.ToLower()).FirstOrDefault();

        public Account? UpdateAccountFromRepository(string accountIBAN)
        {
            Account? account = GetAccountFromRepository(accountIBAN);
            if (account == null)
            {
                return null;
            }
            _dbContext.Accounts.Update(account);
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