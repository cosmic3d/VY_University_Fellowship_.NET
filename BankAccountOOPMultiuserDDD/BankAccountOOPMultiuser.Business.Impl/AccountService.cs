using BankAccountOOPMultiuser.Business.Contracts;

namespace BankAccountOOPMultiuser.Business.Impl
{
    public class AccountService : IAccountService
    {
        IncomeResultDto IAccountService.AddMoney(decimal income)
        {
            throw new NotImplementedException();
        }

        List<Tuple<DateTime, int>> IAccountService.GetIncomes()
        {
            throw new NotImplementedException();
        }

        decimal? IAccountService.GetMoney()
        {
            throw new NotImplementedException();
        }

        List<Tuple<DateTime, int>> IAccountService.GetMovements()
        {
            throw new NotImplementedException();
        }

        List<Tuple<DateTime, int>> IAccountService.GetOutcomes()
        {
            throw new NotImplementedException();
        }

        OutcomeResultDto IAccountService.RetireMoney(decimal outcome)
        {
            throw new NotImplementedException();
        }
    }
}
