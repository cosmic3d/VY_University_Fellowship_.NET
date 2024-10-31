using System;

public static class AccountModelValidator
{
    public const decimal maxIncome = 5000;
    public const decimal maxOutcome = 1000;
    public const decimal maxDebtAllowed = -200;
    public static bool NegativeOrZeroError { get; private set; }
    public static bool MaxIncomeSurpassedError { get; private set; }
    public static bool MaxOutcomeSurpassedError { get; private set; }
    public static bool OutcomeLeavesAccountOnRedError { get; private set; }

    public static bool ValidateIncome(int income)
    {
        NegativeOrZeroError = income <= 0;
        MaxIncomeSurpassedError = income > maxIncome;

        return !NegativeOrZeroError && !MaxOutcomeSurpassedError
    }

    public static bool ValidateOutcome(int outcome, int money) {
        NegativeOrZeroError = outcome <= 0;
        MaxOutcomeSurpassedError = outcome > maxOutcome;
        OutcomeLeavesAccountOnRedError = money - outcome < maxDebtAllowed;

        return !NegativeOrZeroError && !MaxOutcomeSurpassedError && !OutcomeLeavesAccountOnRedError;
    }

    public void Reset() {
        NegativeOrZeroError = false;
        MaxIncomeSurpassedError = false;
        MaxOutcomeSurpassedError = false;
        OutcomeLeavesAccountOnRedError = false;
    }
}
