using System;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
	public class OutcomeResultDto
	{
		public bool HasErrors;
		public OutcomeErrorEnum OutcomeResultError;
		public decimal MaxOutcomeAllowed;
		public decimal TotalMoney;
		public decimal MaxDebtAllowed;

	}

}
