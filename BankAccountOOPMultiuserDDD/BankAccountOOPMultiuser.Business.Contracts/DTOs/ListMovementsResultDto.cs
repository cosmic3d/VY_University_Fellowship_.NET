using System;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
	public class ListMovementsResultDto
	{
		public bool HasErrors;
		public ListMovementsErrorEnum ListMovementsResultError;
		public decimal TotalMoney { get; set; }
		public List<Tuple<DateTime, Decimal>>? MovementList;
	}

}
