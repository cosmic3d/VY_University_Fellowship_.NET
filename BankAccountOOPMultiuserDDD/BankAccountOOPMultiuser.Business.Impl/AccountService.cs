using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Domain.Models;
using BankAccountOOPMultiuser.Domain.Models.Validators;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using BankAccountOOPMultiuser.Infrastructure.Impl;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Impl
{
    public class AccountService : IAccountService
    {
        private AccountModelValidator _validator;
        private IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _validator = new AccountModelValidator();
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

        //method for retrieving all the accounts
        List<FullAccountDto>? IAccountService.GetAccounts()
        {
            List<Account>? accounts = _accountRepository.GetAllAccounts();
            if (accounts == null || accounts.Count() == 0) return null;
            List<FullAccountDto> fullAccountDtos = new();
            foreach (var account in accounts)
            {
                fullAccountDtos.Add(new FullAccountDto()
                {
                    Id = account.Id,
                    Balance = account.Balance,
                    Iban = account.Iban,
                    Pin = account.Pin,
                    Movements = AccountRepository.MovementCollectionToList(account)
                });
            }
            return fullAccountDtos;
        }

        FullAccountDto? IAccountService.GetAccount(string iban)
        {
            Account? account = _accountRepository.GetAccountFromRepository(iban);
            if (account == null) return null;
            return new FullAccountDto()
            {
                Id = account.Id,
                Balance = account.Balance,
                Iban = account.Iban,
                Pin = account.Pin,
                Movements = AccountRepository.MovementCollectionToList(account)
            };
        }

        FullAccountDto? IAccountService.AddAccount(NewAccountDto account)
        {
            FullAccountDto fullAccountDto = new FullAccountDto();
            if (account == null) return null;
            else if (_accountRepository.GetAccountFromRepository(account.Iban) is Account accountEntity)
            {
                fullAccountDto.HasErrors = true;
                fullAccountDto.AccountError = AccountErrorEnum.AccountAlreadyExistsError;
                return fullAccountDto;
            };
            AccountModel accountModel = new(0, new(), account.Iban, account.Pin);
            if (_validator.ValidateNewAccount(accountModel))
            {
                Account newAccount = new()
                {
                    Balance = accountModel.Balance,
                    Iban = accountModel.Iban,
                    Pin = accountModel.Pin
                };
                _accountRepository.AddAccountToRepository(newAccount);
                fullAccountDto.Id = newAccount.Id;
                fullAccountDto.Balance = newAccount.Balance;
                fullAccountDto.Iban = newAccount.Iban;
                fullAccountDto.Pin = newAccount.Pin;
                fullAccountDto.Movements = AccountRepository.MovementCollectionToList(newAccount);
                return fullAccountDto;
            }
            else
            {
                fullAccountDto.HasErrors = true;
                fullAccountDto.AccountError = _validator.IBANMustBeginWithIBANError ? AccountErrorEnum.IBANMustBeginWithIBANError
                                              : _validator.IBANLengthError ? AccountErrorEnum.IBANLengthError
                                              : AccountErrorEnum.PinMustBe4DigitsError;
                return fullAccountDto;
            }
        }
        IncomeResultDto IAccountService.AddMoney(decimal income, string iban)
        {
            AccountModel accountModel;
            Account? accountEntity;
            IncomeResultDto resultDto = new ();
            if (_validator.ValidateIncome(income))
            {
                accountEntity = _accountRepository.GetAccountFromRepository(iban);
                if (accountEntity == null)
                {
                    resultDto.HasErrors = true;
                    resultDto.IncomeResultError = IncomeErrorEnum.AccountNotFound;
                }
                else
                {
                    accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
                        accountEntity.Pin
                        );
                    accountModel.AddIncome( income );
                    accountEntity.Balance = accountModel.Balance;
                    _accountRepository.AddMovementToAccount(accountEntity, new(DateTime.Now, income));
                    resultDto.TotalMoney = accountEntity.Balance;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.IncomeResultError = _validator.MaxIncomeSurpassedError ? IncomeErrorEnum.MaxIncomeSurpassed : IncomeErrorEnum.NegativeOrZero;
                resultDto.MaxIncomeAllowed = AccountModelValidator.maxIncome;
            }
            return resultDto;
        }
        OutcomeResultDto IAccountService.RetireMoney(decimal outcome, string iban)
        {
            AccountModel accountModel;
            OutcomeResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(iban) is Account accountEntity)
            {
                if (_validator.ValidateOutcome(outcome, accountEntity.Balance))
                {
                    accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
                        accountEntity.Pin
                        );
                    accountModel.AddOutcome(outcome);
                    accountEntity.Balance = accountModel.Balance;
                    _accountRepository.AddMovementToAccount(accountEntity, new(DateTime.Now, outcome));
                    resultDto.TotalMoney = accountEntity.Balance;
                }
                else
                {
                    resultDto.HasErrors = true;
                    resultDto.OutcomeResultError = _validator.MaxOutcomeSurpassedError ? OutcomeErrorEnum.MaxOutcomeSurpassed : OutcomeErrorEnum.OutcomeLeavesAccountOnRed;
                    resultDto.MaxOutcomeAllowed = AccountModelValidator.maxOutcome;
                    resultDto.MaxDebtAllowed = AccountModelValidator.maxDebtAllowed;
                }
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.OutcomeResultError = OutcomeErrorEnum.AccountNotFound;
            }
            return resultDto;
        }


        ListMovementsResultDto IAccountService.GetMovements(string iban)
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new ();
            if (_accountRepository.GetAccountFromRepository(iban) is Account accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
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

        ListMovementsResultDto IAccountService.GetIncomes(string iban)
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(iban) is Account accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
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

        ListMovementsResultDto IAccountService.GetOutcomes(string iban)
        {
            AccountModel accountModel;
            ListMovementsResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(iban) is Account accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
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
        MoneyResultDto IAccountService.GetMoney(string iban)
        {
            AccountModel accountModel;
            MoneyResultDto resultDto = new();
            if (_accountRepository.GetAccountFromRepository(iban) is Account accountEntity)
            {
                resultDto.TotalMoney = accountEntity.Balance;
                accountModel = new(
                        accountEntity.Balance,
                        AccountRepository.MovementCollectionToList(accountEntity),
                        accountEntity.Iban,
                        accountEntity.Pin
                        );
                resultDto.TotalMoney = accountModel.GetBalance();
            }
            else
            {
                resultDto.HasErrors = true;
                resultDto.MoneyResultError = MoneyErrorEnum.AccountNotFound;
            }
            return resultDto;
        }

    }
}
