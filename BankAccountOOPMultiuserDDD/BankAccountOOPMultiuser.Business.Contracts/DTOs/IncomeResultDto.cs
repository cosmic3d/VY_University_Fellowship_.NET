using System;
using BankAccountOOPMultiuser.XCutting.Enums;

public class IncomeResultDto
{
	public bool HasErrors;
	public IncomeErrorEnum incomeResultError;
	public int MaxIncomeAllowed;
	public int TotalMoney;

}
