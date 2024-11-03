using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
    public class LoginDto
    {
        public bool HasErrors;
        public LoginErrorEnum LoginError;
        public string AccountIBAN;
    }
}
