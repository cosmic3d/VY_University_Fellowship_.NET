using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        public static uint AccountIdx = 0;
        private static List<AccountEntity> simulatedAccountDBTable = new();

        public AccountEntity AddAccountToRepository(AccountEntity account)
        {
            account.Id = AccountIdx++;
            account.Movements = new();
            simulatedAccountDBTable.Add(account);
            return account;
        }

        public AccountEntity? GetAccountFromRepository(string accountIBAN) =>
            simulatedAccountDBTable.Where(acc => acc.IBAN.ToLower() == accountIBAN.ToLower()).FirstOrDefault();

        public void UpdateAccountFromRepository(AccountEntity account)
        {
            AccountEntity? accountEntity;
            accountEntity = simulatedAccountDBTable.Where(acc => acc.Id == account.Id).First();
            accountEntity.Balance = account.Balance;
            accountEntity.Movements = account.Movements;
        }
    }

}