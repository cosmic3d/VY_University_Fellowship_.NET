using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        Account AddAccountToRepository(Account account);
        public Account? GetAccountFromRepository(string accountIBAN);
        public Account? AddMovementToAccount(Account account, Tuple<DateTime, Decimal> movement);
        public List<Account>? GetAllAccounts();
    }
}