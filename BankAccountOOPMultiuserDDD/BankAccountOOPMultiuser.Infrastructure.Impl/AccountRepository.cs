using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        public static uint AccountIdx = 0;
        private static List<AccountEntity> simulatedAccountDBTable = new()
        {
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0001", Pin = "0001", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0002", Pin = "0002", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0003", Pin = "0003", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0004", Pin = "0004", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0005", Pin = "0005", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0006", Pin = "0006", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0007", Pin = "0007", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0008", Pin = "0008", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0009", Pin = "0009", Balance = 0, Movements = new() },
            new AccountEntity { Id = AccountIdx++, IBAN = "IBAN0010", Pin = "0010", Balance = 0, Movements = new() }
        };

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