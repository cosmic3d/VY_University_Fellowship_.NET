using BankAccountOOPMultiuser.Business.Contracts.DTOs;

namespace BankAccountOOPMultiuser.Business.Contracts
{
    public interface IAccountService
    {
        public LoginDto CheckPIN(string accountPIN);
        public LoginDto CheckIBAN(string accountIBAN);
        public decimal? GetMoney();
        public IncomeResultDto AddMoney(decimal income);
        public OutcomeResultDto RetireMoney(decimal outcome);
        public ListMovementsResultDto GetMovements();
        public ListMovementsResultDto GetIncomes();
        public ListMovementsResultDto GetOutcomes();
    }
}
