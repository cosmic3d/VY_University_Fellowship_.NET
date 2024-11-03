using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Business.Impl;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Impl;
using BankAccountOOPMultiuser.Presentation.ConsoleUI;
using BankAccountOOPMultiuser.Presentation.ConsoleUI.Menu;
using BankAccountOOPMultiuser.XCutting.Enums;
using BankAccountOOPMultiuser.XCutting.Session;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new ServiceCollection();
ServiceProvider serviceProvider = services
                                  .AddScoped<IAccountRepository, AccountRepository>()
                                  .AddScoped<IAccountService, AccountService>()
                                  .AddScoped<MainMenu>()
                                  .BuildServiceProvider();

MainMenu mainMenu = serviceProvider.GetService<MainMenu>();

if (mainMenu == null)
{
    Console.WriteLine("Service Provider error: No such service");
}
else
{
    while (true)
    {
        LoginDto loginDto = new();

        Console.Clear();
        Console.WriteLine("Login page");
        while (Session.IBAN == null)
        {
            string outputMsg = "";
            string requestIBAN = InputParsing.GetString("Enter account IBAN")!;
            loginDto = mainMenu.AccountService.CheckIBAN(requestIBAN);
            if (loginDto.HasErrors)
            {
                switch (loginDto.LoginError)
                {
                    case LoginErrorEnum.AccountNotFound:
                        outputMsg += $"No account with IBAN ${loginDto.AccountIBAN} was found";
                        break;
                }
                Console.WriteLine(outputMsg);
            }
        }
        while (Session.Pin == null)
        {
            string outputMsg = "";
            string requestPin = InputParsing.GetString("Enter account Pin")!;
            loginDto = mainMenu.AccountService.CheckPIN(requestPin);
            if (loginDto.HasErrors)
            {
                switch (loginDto.LoginError)
                {
                    case LoginErrorEnum.AccountNotFound:
                        outputMsg += $"No account with IBAN ${loginDto.AccountIBAN} was found";
                        break;
                    case LoginErrorEnum.IncorrectPin:
                        outputMsg += $"Account Pin incorrect for account with IBAN {loginDto.AccountIBAN}";
                        break;
                }
                Console.WriteLine(outputMsg);
            }
        }
        mainMenu.RunMenu();
        Session.IBAN = null;
        Session.Pin = null;
    }
}