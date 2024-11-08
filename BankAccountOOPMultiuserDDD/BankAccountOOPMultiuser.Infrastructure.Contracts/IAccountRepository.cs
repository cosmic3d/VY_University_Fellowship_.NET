using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        AccountEntity AddAccountToRepository(AccountEntity account);
        public AccountEntity? GetAccountFromRepository(string accountIBAN);
        public AccountEntity? AddMovementToAccount(AccountEntity account, Tuple<DateTime, Decimal> movement);
        public List<AccountEntity>? GetAllAccounts();

        public void DeleteAccountFromRepository(AccountEntity account);
    }
}