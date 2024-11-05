using BankAccountOOPMultiuser.Infrastructure.Contracts.Datos;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOOPMultiuser.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MyFirstDataBaseContext _dbContext;
        public AccountsController(MyFirstDataBaseContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public string GetAccounts()
        {
            Account account;
            account = _dbContext.Accounts.First();
            return $"Name: {account.Iban}{Environment.NewLine}Balance: {account.Balance}";
        }
    }
}
