using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
    public class MoneyResultDto
    {
        public bool HasErrors;
        public MoneyErrorEnum MoneyResultError;
        public decimal TotalMoney;

    }

}