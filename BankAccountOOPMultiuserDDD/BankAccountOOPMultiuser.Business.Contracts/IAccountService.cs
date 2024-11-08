using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Business.Contracts
{
    public interface IAccountService
    {
        //public LoginDto CheckPIN(string accountPIN);
        //public LoginDto CheckIBAN(string accountIBAN);
        MoneyResultDto GetMoney(string iban);
        public IncomeResultDto AddMoney(decimal income, string iban);
        public OutcomeResultDto RetireMoney(decimal outcome, string iban);
        public ListMovementsResultDto GetMovements(string iban);
        public ListMovementsResultDto GetIncomes(string iban);
        public ListMovementsResultDto GetOutcomes(string iban);

        public List<FullAccountDto>? GetAccounts();

        public FullAccountDto? GetAccount(string iban);

        public FullAccountDto? AddAccount(NewAccountDto account);

        public FullAccountDto? DeleteAccount(string iban);
    }
}
