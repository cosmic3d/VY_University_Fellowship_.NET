using System;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
	public class IncomeResultDto
	{
		public bool HasErrors;
		public IncomeErrorEnum IncomeResultError;
		public decimal MaxIncomeAllowed;
		public decimal TotalMoney;

	}

}
