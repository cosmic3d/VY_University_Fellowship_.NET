using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        AccountEntity AddAccountToRepository(AccountEntity account);
        public AccountEntity? GetAccountFromRepository(string accountIBAN);
        public void UpdateAccountFromRepository(AccountEntity account);
    }
}
