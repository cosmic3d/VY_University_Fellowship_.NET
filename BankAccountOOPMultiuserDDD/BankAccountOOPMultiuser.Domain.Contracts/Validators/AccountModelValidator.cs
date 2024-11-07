using System;
using System.Text.RegularExpressions;

namespace BankAccountOOPMultiuser.Domain.Models.Validators
{
    public class AccountModelValidator
    {
        public const decimal maxIncome = 5000;
        public const decimal maxOutcome = 1000;
        public const decimal maxDebtAllowed = -200;
        public bool NegativeOrZeroError { get; private set; }
        public bool MaxIncomeSurpassedError { get; private set; }
        public bool MaxOutcomeSurpassedError { get; private set; }
        public bool OutcomeLeavesAccountOnRedError { get; private set; }
        public bool IBANMustBeginWithIBANError { get; private set; }
        public bool IBANLengthError { get; private set; }
        public bool PinMustBe4DigitsError { get; private set; }

        public bool ValidateIncome(decimal income)
        {
            NegativeOrZeroError = income <= 0;
            MaxIncomeSurpassedError = income > maxIncome;

            return !NegativeOrZeroError && !MaxIncomeSurpassedError;
        }

        public bool ValidateOutcome(decimal outcome, decimal money) {
            outcome = Math.Abs(outcome);
            MaxOutcomeSurpassedError = outcome > maxOutcome;
            OutcomeLeavesAccountOnRedError = money - outcome < maxDebtAllowed;

            return !MaxOutcomeSurpassedError && !OutcomeLeavesAccountOnRedError;
        }

        public bool ValidateNewAccount(AccountModel account)
        {
            IBANMustBeginWithIBANError = !Regex.IsMatch(account.Iban.ToUpper(), @"^IBAN");
            IBANLengthError = !(account.Iban.Length >= 8 && account.Iban.Length <= 24);
            PinMustBe4DigitsError = !Regex.IsMatch(account.Pin, @"^\d{4}$");
            return !IBANMustBeginWithIBANError &&
                   !IBANLengthError &&
                   !PinMustBe4DigitsError;
        }
    }

}
