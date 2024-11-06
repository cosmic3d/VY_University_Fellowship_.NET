using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.XCutting.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOOPMultiuser.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet(Name = "GetAllAccounts")]
        public ActionResult<FullAccountDto> GetAccounts()
        {
            List<FullAccountDto>? accounts = _accountService.GetAccounts();
            if (accounts is null) return StatusCode(StatusCodes.Status404NotFound, "No accounts found");
            return Ok(accounts);
        }

        [HttpGet("{iban}", Name = "GetAccountByIban")]
        public ActionResult<FullAccountDto> GetAccount(string iban)
        {
            FullAccountDto? account = _accountService.GetAccount(iban);
            if (account is null) return StatusCode(StatusCodes.Status404NotFound, "Account not found");
            return Ok(account);
        }

        [HttpPost(Name = "AddAccount")]
        public ActionResult<FullAccountDto> AddAccount(NewAccountDto account)
        {
            FullAccountDto? newAccount = _accountService.AddAccount(account);
            if (newAccount is null) return StatusCode(StatusCodes.Status400BadRequest, "Account not created");
            else if (newAccount.HasErrors)
            {
                switch (newAccount.AccountError)
                {
                    case AccountErrorEnum.AccountAlreadyExistsError:
                        return StatusCode(StatusCodes.Status409Conflict, "IBAN already exists");
                    case AccountErrorEnum.IBANMustBeginWithIBANError:
                        return StatusCode(StatusCodes.Status400BadRequest, "IBAN must begin with IBAN");
                    case AccountErrorEnum.IBANLengthError:
                        return StatusCode(StatusCodes.Status400BadRequest, "IBAN length must have a minimum length of 8 characters and a maximum length of 24 characters");
                    case AccountErrorEnum.PinMustBe4DigitsError:
                        return StatusCode(StatusCodes.Status400BadRequest, "PIN must be a 4 digit number");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Account not created");
                }
            }
            return Ok(newAccount);
        }

        [HttpPost("{iban}/income", Name = "AddIncome")]
        public ActionResult<IncomeResultDto> AddIncome(decimal income, string iban)
        {
            IncomeResultDto result = _accountService.AddMoney(income, iban);
            if (result.HasErrors)
            {
                switch (result.IncomeResultError)
                {
                    case IncomeErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    case IncomeErrorEnum.MaxIncomeSurpassed:
                        return StatusCode(StatusCodes.Status400BadRequest, $"Income surpasses the maximum allowed ({result.MaxIncomeAllowed})");
                    case IncomeErrorEnum.NegativeOrZero:
                        return StatusCode(StatusCodes.Status400BadRequest, "Income must be greater than zero");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Income not added");
                }
            }
            return Ok(result);
        }

        [HttpPost("{iban}/outcome", Name = "AddOutcome")]
        public ActionResult<OutcomeResultDto> AddOutcome(decimal outcome, string iban)
        {
            OutcomeResultDto result = _accountService.RetireMoney(outcome, iban);
            if (result.HasErrors)
            {
                switch (result.OutcomeResultError)
                {
                    case OutcomeErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    case OutcomeErrorEnum.MaxOutcomeSurpassed:
                        return StatusCode(StatusCodes.Status400BadRequest, $"Outcome surpasses the maximum allowed ({result.MaxOutcomeAllowed})");
                    case OutcomeErrorEnum.OutcomeLeavesAccountOnRed:
                        return StatusCode(StatusCodes.Status400BadRequest, "Outcome leaves account on red");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Outcome not added");
                }
            }
            return Ok(result);
        }

        [HttpGet("{iban}/movements", Name = "GetMovements")]
        public ActionResult<ListMovementsResultDto> GetMovements(string iban)
        {
            ListMovementsResultDto result = _accountService.GetMovements(iban);
            if (result.HasErrors)
            {
                switch (result.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    case ListMovementsErrorEnum.NoMovementsFound:
                        return StatusCode(StatusCodes.Status400BadRequest, "Movements not found");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Movements not found");
                }
            }
            return Ok(result);
        }

        [HttpGet("{iban}/incomes", Name = "GetIncomes")]
        public ActionResult<ListMovementsResultDto> GetIncomes(string iban)
        {
            ListMovementsResultDto result = _accountService.GetIncomes(iban);
            if (result.HasErrors)
            {
                switch (result.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    case ListMovementsErrorEnum.NoMovementsFound:
                        return StatusCode(StatusCodes.Status400BadRequest, "Incomes not found");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Incomes not found");
                }
            }
            return Ok(result);
        }

        [HttpGet("{iban}/outcomes", Name = "GetOutcomes")]
        public ActionResult<ListMovementsResultDto> GetOutcomes(string iban)
        {
            ListMovementsResultDto result = _accountService.GetOutcomes(iban);
            if (result.HasErrors)
            {
                switch (result.ListMovementsResultError)
                {
                    case ListMovementsErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    case ListMovementsErrorEnum.NoMovementsFound:
                        return StatusCode(StatusCodes.Status400BadRequest, "Outcomes not found");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Outcomes not found");
                }
            }
            return Ok(result);
        }

        [HttpGet("{iban}/balance", Name = "GetBalance")]
        public ActionResult<MoneyResultDto> GetBalance(string iban)
        {
            MoneyResultDto result = _accountService.GetMoney(iban);
            if (result.HasErrors)
            {
                switch (result.MoneyResultError)
                {
                    case MoneyErrorEnum.AccountNotFound:
                        return StatusCode(StatusCodes.Status404NotFound, "Account not found");
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, "Balance not found");
                }
            }
            return Ok(result);
        }


    }
}
