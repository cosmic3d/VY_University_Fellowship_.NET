using System;
using System.Text.RegularExpressions;

namespace BankAccountOOPMultiuser.Domain.Models.Validators
{
    public static class AccountModelValidator
    {
        public const decimal maxIncome = 5000;
        public const decimal maxOutcome = 1000;
        public const decimal maxDebtAllowed = -200;
        public static bool NegativeOrZeroError { get; private set; }
        public static bool MaxIncomeSurpassedError { get; private set; }
        public static bool MaxOutcomeSurpassedError { get; private set; }
        public static bool OutcomeLeavesAccountOnRedError { get; private set; }
        public static bool IBANMustBeginWithIBANError { get; private set; }
        public static bool IBANLengthError { get; private set; }
        public static bool PinMustBe4DigitsError { get; private set; }

        public static bool ValidateIncome(decimal income)
        {
            NegativeOrZeroError = income <= 0;
            MaxIncomeSurpassedError = income > maxIncome;

            return !NegativeOrZeroError && !MaxIncomeSurpassedError;
        }

        public static bool ValidateOutcome(decimal outcome, decimal money) {
            outcome = Math.Abs(outcome);
            MaxOutcomeSurpassedError = outcome > maxOutcome;
            OutcomeLeavesAccountOnRedError = money - outcome < maxDebtAllowed;

            return !MaxOutcomeSurpassedError && !OutcomeLeavesAccountOnRedError;
        }

        public static bool ValidateNewAccount(AccountModel account)
        {
            // validate all this 'IBANMustBeginWithIBANError,
            //    IBANLengthError,
            //PinMustBe4DigitsError,'
            return Regex.IsMatch(account.Iban, @"^IBAN") && (account.Iban.Length >= 8 && account.Iban.Length <= 24) && account.Pin.Length == 4;
        }

        public static void Reset() {
            NegativeOrZeroError = false;
            MaxIncomeSurpassedError = false;
            MaxOutcomeSurpassedError = false;
            OutcomeLeavesAccountOnRedError = false;
        }
    }

}
