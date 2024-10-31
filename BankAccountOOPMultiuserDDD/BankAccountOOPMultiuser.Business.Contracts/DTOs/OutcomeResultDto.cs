using System;
using BankAccountOOPMultiuser.XCutting.Enums;

public class OutcomeResultDto
{
	public bool HasErrors;
	public OutcomeErrorEnum incomeResultError;
	public int MaxOutcomeAllowed;
	public int TotalMoney;

}
