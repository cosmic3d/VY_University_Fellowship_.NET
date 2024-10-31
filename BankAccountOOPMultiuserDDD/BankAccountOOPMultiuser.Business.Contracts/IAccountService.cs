using BankAccountOOPMultiuser.Business.Contracts.

namespace BankAccountOOPMultiuser.Business.Contracts
{
    public interface IAccountService
    {
        decimal? GetMoney();
        IncomeResultDto AddMoney(decimal income);
        OutcomeResultDto RetireMoney(decimal outcome);
        List<Tuple<DateTime, int>> GetMovements();
        List<Tuple<DateTime, int>> GetIncomes();
        List<Tuple<DateTime, int>> GetOutcomes();
    }
}
