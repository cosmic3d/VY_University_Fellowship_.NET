using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Domain.Models;
using BankAccountOOPMultiuser.Domain.Models.Validators;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using BankAccountOOPMultiuser.XCutting.Enums;
using BankAccountOOPMultiuser.XCutting.Session;

namespace BankAccountOOPMultiuser.Business.Impl
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //LoginDto IAccountService.CheckIBAN(string accountIBAN)
        //{
        //    LoginDto loginDto = new();
        //    if (_accountRepository.GetAccountFromRepository(accountIBAN) is Account accountEntity)
        //    {
        //        Session.IBAN = accountEntity.IBAN;
        //    }
        //    else
        //    {
        //        loginDto.HasErrors = true;
        //        loginDto.LoginError = LoginErrorEnum.AccountNotFound;
        //        loginDto.AccountIBAN = accountIBAN;
        //    }
        //    return loginDto;
        //}
        //LoginDto IAccountService.CheckPIN(string requestPin)
        //{
        //    AccountModel accountModel;
        //    LoginDto loginDto = new();
        //    if (_accountRepository.GetAccountFromRepository(Session.IBAN!) is Account accountEntity)
        //    {
        //        accountModel = new(
        //                accountEntity.Balance,
        //                accountEntity.Movements,
        //                accountEntity.Pin
        //                );
        //        if (accountModel.CorrectPin(requestPin))
        //        {
        //            Session.Pin = requestPin;
        //        }
        //        else
        //        {
        //            loginDto.HasErrors = true;
        //            loginDto.LoginError = LoginErrorEnum.IncorrectPin;
        //            loginDto.AccountIBAN = Session.IBAN!;
        //        }
        //    }
        //    else
        //    {
        //        loginDto.HasErrors = true;
        //        loginDto.LoginError = LoginErrorEnum.AccountNotFound;
        //        loginDto.AccountIBAN = Session.IBAN!; //probably null
        //    }
        //    return loginDto;
        //}

        IncomeResultDto IAccountService.AddMoney(decimal income)
        {
            AccountModel accountModel;
            Account? accountEntity;
            IncomeResultDto resultDto = new ();
            if (AccountModelValidator.ValidateIncome(income))
            {
                accountEntity = _accountRepository.GetAccountFromRepository(Session.IBAN!);
                if (accountEntity == null)
                {
                    resultDto.HasErrors = true;
                    resultDto.IncomeResultError = IncomeErrorEnum.AccountNotFound;
                }
                else
                {
                    accountModel = new(
                        accountEntity.Balance,
                        accountEntity.Movements,
                        accountEntity.Pin
                        );
                    accountModel.AddIncome( income );
                    accountEntity.Balance = accountModel.Balance;
                    accountEntity.Movements = accountModel.Movements;
                    resultDto.TotalMoney = accountEntity.Balance;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.IncomeResultError = AccountModelValidator.MaxIncomeSurpassedError ? IncomeErrorEnum.MaxIncomeSurpassed : IncomeErrorEnum.NegativeOrZero;
                resultDto.MaxIncomeAllowed = AccountModelValidator.maxIncome;
            }
            AccountModelValidator.Reset();
            return resultDto;
        }
        OutcomeResultDto IAccountService.RetireMoney(decimal outcome)
        {
            AccountModel accountModel;
            OutcomeResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(Session.IBAN!) is AccountEntity accountEntity)
            {
                if (AccountModelValidator.ValidateOutcome(outcome, accountEntity.Balance))
                {
                    accountModel = new(
                        accountEntity.Balance,
                        accountEntity.Movements,
                        accountEntity.Pin
                        );
                    accountModel.AddOutcome(outcome);
                    accountEntity.Balance = accountModel.Balance;
                    accountEntity.Movements = accountModel.Movements;
                    resultDto.TotalMoney = accountEntity.Balance;
                }
                else
                {
                    resultDto.HasErrors = true;
                    resultDto.OutcomeResultError = AccountModelValidator.MaxOutcomeSurpassedError ? OutcomeErrorEnum.MaxOutcomeSurpassed
                        : AccountModelValidator.NegativeOrZeroError ? OutcomeErrorEnum.NegativeOrZero
                        : OutcomeErrorEnum.OutcomeLeavesAccountOnRed;
                    resultDto.MaxOutcomeAllowed = AccountModelValidator.maxOutcome;
                    resultDto.MaxDebtAllowed = AccountModelValidator.maxDebtAllowed;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.OutcomeResultError = OutcomeErrorEnum.AccountNotFound;
            }
            AccountModelValidator.Reset();
            return resultDto;
        }


        ListMovementsResultDto IAccountService.GetMovements()
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new ();
            if (_accountRepository.GetAccountFromRepository(Session.IBAN!) is AccountEntity accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        accountEntity.Movements,
                        accountEntity.Pin
                        );
                if (accountModel.GetAllMovements() is List<Tuple<DateTime, decimal>> accountMovements)
                {
                    resultDto.MovementList = accountMovements;
                }
                else
                {
                    resultDto.HasErrors = true;
                    resultDto.ListMovementsResultError = ListMovementsErrorEnum.NoMovementsFound;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.ListMovementsResultError = ListMovementsErrorEnum.AccountNotFound;
            }
            return resultDto;
        }

        ListMovementsResultDto IAccountService.GetIncomes()
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(Session.IBAN!) is AccountEntity accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        accountEntity.Movements,
                        accountEntity.Pin
                        );
                if (accountModel.GetAllIncomes() is List<Tuple<DateTime, decimal>> accountMovements)
                {
                    resultDto.MovementList = accountMovements;
                }
                else
                {
                    resultDto.HasErrors = true;
                    resultDto.ListMovementsResultError = ListMovementsErrorEnum.NoMovementsFound;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.ListMovementsResultError = ListMovementsErrorEnum.AccountNotFound;
            }
            return resultDto;
        }

        ListMovementsResultDto IAccountService.GetOutcomes()
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(Session.IBAN!) is AccountEntity accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        accountEntity.Movements,
                        accountEntity.Pin
                        );
                if (accountModel.GetAllOutcomes() is List<Tuple<DateTime, decimal>> accountMovements)
                {
                    resultDto.MovementList = accountMovements;
                }
                else
                {
                    resultDto.HasErrors = true;
                    resultDto.ListMovementsResultError = ListMovementsErrorEnum.NoMovementsFound;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.ListMovementsResultError = ListMovementsErrorEnum.AccountNotFound;
            }
            return resultDto;
        }
        decimal? IAccountService.GetMoney()
        {
            throw new NotImplementedException();
        }

    }
}
