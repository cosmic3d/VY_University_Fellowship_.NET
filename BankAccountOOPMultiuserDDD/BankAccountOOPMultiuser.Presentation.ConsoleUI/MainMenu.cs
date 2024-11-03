using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Presentation.ConsoleUI.Menu;
using BankAccountOOPMultiuser.XCutting.Enums;
namespace BankAccountOOPMultiuser.Presentation.ConsoleUI
{
    internal class MainMenu : Menu.Menu
    {
        public IAccountService AccountService { get; set; }
        public MainMenu(IAccountService accountService)
        {
            AccountService = accountService;
            AddMethod(1, "Income money", AddMoney);
            AddMethod(2, "Outcome money", RetrieveMoney);
            AddMethod(3, "List all movements", ListAllMovements);
            AddMethod(4, "List incomes", ListIncomes);
            AddMethod(5, "List outcomes", ListOutcomes);
            AddMethod(6, "Show current balance", ShowBalance);
            AddMethod(7, "Exit program", ExitProgram);
        }

        private void AddMoney()
        {
            string outputMsg = "";
            decimal income = InputParsing.GetDecimal("Enter a quantity to income");
            IncomeResultDto result = AccountService.AddMoney(income);
            if (result.HasErrors)
            {
                switch (result.IncomeResultError)
                {
                    case IncomeErrorEnum.MaxIncomeSurpassed:
                        outputMsg = $"You surpassed the max income of {result.MaxIncomeAllowed}€.";
                        break;
                    case IncomeErrorEnum.NegativeOrZero:
                        outputMsg = $"Your income must be negative of zero.";
                        break;
                    case IncomeErrorEnum.AccountNotFound:
                        outputMsg = $"Account was not found";
                        break;
                }
                outputMsg += " No operation was performed";
            }
            else
            {
                outputMsg = "Operation was succesfully performed";
                outputMsg += $"{Environment.NewLine}Your current balance is {result.TotalMoney}€";
            }
            Console.WriteLine(outputMsg);
        }
        private void RetrieveMoney()
        {
            string outputMsg = "";
            decimal outcome = InputParsing.GetDecimal("Enter a quantity to outcome");
            OutcomeResultDto result = AccountService.RetireMoney(outcome);
            if (result.HasErrors)
            {
                switch (result.OutcomeResultError)
                {
                    case OutcomeErrorEnum.MaxOutcomeSurpassed:
                        outputMsg = $"You surpassed the max outcome of {result.MaxOutcomeAllowed}€.";
                        break;
                    case OutcomeErrorEnum.NegativeOrZero:
                        outputMsg = $"Your outcome must be negative of zero.";
                        break;
                    case OutcomeErrorEnum.OutcomeLeavesAccountOnRed:
                        outputMsg = $"You cannot outcome that much money. You are lacking {Math.Abs(result.TotalMoney - Math.Abs(outcome)) - result.MaxDebtAllowed}";
                        break;
                    case OutcomeErrorEnum.AccountNotFound:
                        outputMsg = $"Account was not found";
                        break;
                }
                outputMsg += " No operation was performed";
            }
            else
            {
                outputMsg = "Operation was succesfully performed";
                outputMsg += $"{Environment.NewLine}Your current balance is {result.TotalMoney}€";
            }
            Console.WriteLine(outputMsg);
        }

        private void ListAllMovements()
        {
            string outputMsg = "";
            ListMovementsResultDto resultDto = AccountService.GetMovements();
            if (resultDto.HasErrors)
            {
                switch (resultDto.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.NoMovementsFound:
                        outputMsg += "No movements were found on your account.";
                        break;
                    case ListMovementsErrorEnum.AccountNotFound:
                        outputMsg += "Account was not found.";
                        break;
                }
                outputMsg += " No operation was performed";
            }
            else
            {
                foreach (var movement in resultDto.MovementList)
                {
                    outputMsg += $"{movement.Item1} - {movement.Item2}{Environment.NewLine}";
                }
                outputMsg += "Operation was succesfully performed";
                outputMsg += $"{Environment.NewLine}Your current balance is {resultDto.TotalMoney}€";
            }
            Console.WriteLine(outputMsg);
        }

        private void ListIncomes()
        {
            string outputMsg = "";
            ListMovementsResultDto resultDto = AccountService.GetIncomes();
            if (resultDto.HasErrors)
            {
                switch (resultDto.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.NoMovementsFound:
                        outputMsg += "No incomes were found on your account.";
                        break;
                    case ListMovementsErrorEnum.AccountNotFound:
                        outputMsg += "Account was not found.";
                        break;
                }
                outputMsg += " No operation was performed";
            }
            else
            {
                foreach (var movement in resultDto.MovementList)
                {
                    outputMsg += $"{movement.Item1} - {movement.Item2}{Environment.NewLine}";
                }
                outputMsg += "Operation was succesfully performed";
                outputMsg += $"{Environment.NewLine}Your current balance is {resultDto.TotalMoney}€";
            }
            Console.WriteLine(outputMsg);
        }


        private void ListOutcomes()
        {
            string outputMsg = "";
            ListMovementsResultDto resultDto = AccountService.GetOutcomes();
            if (resultDto.HasErrors)
            {
                switch (resultDto.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.NoMovementsFound:
                        outputMsg += "No outcomes were found on your account.";
                        break;
                    case ListMovementsErrorEnum.AccountNotFound:
                        outputMsg += "Account was not found.";
                        break;
                }
                outputMsg += " No operation was performed";
            }
            else
            {
                foreach (var movement in resultDto.MovementList)
                {
                    outputMsg += $"{movement.Item1} - {movement.Item2}{Environment.NewLine}";
                }
                outputMsg += "Operation was succesfully performed";
                outputMsg += $"{Environment.NewLine}Your current balance is {resultDto.TotalMoney}€";
            }
            Console.WriteLine(outputMsg);
        }
        private void ShowBalance()
        {
            throw new NotImplementedException();
        }

    }
}
