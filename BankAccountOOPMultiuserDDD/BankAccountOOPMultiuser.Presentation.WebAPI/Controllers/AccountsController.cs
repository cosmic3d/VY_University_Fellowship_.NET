using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
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

        [HttpGet]
        public ActionResult<FullAccountDto> GetAccounts()
        {
            List<FullAccountDto>? accounts = _accountService.GetAccounts();
            if (accounts is null) return StatusCode(StatusCodes.Status404NotFound, "No accounts found");
            return Ok(accounts);
        }

        [HttpGet("{iban}")]
        public ActionResult<FullAccountDto> GetAccount(string iban)
        {
            FullAccountDto? account = _accountService.GetAccount(iban);
            if (account is null) return StatusCode(StatusCodes.Status404NotFound, "Account not found");
            return Ok(account);
        }

        //[HttpPost]
        //public ActionResult<AccountDto> AddAccount([FromBody] AccountDto account)
        //{
        //    AccountDto? newAccount = _accountService.AddAccount(account);
        //    if (newAccount is null) return StatusCode(StatusCodes.Status400BadRequest, "Account not added");
        //    return Ok(newAccount);
        //}


    }
}
