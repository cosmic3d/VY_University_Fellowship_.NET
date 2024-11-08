using BankAccountOOPMultiuser.Domain.Models;
using BankAccountOOPMultiuser.Domain.Models.Validators;

namespace BankAccountOOPMultiuser.Testing.UnitTesting.Domain
{
    public class AccountModelUnitTests
    {
        #region IsValidIncome
        [Fact]
        public void IsValidIncome_WhenValidIncome_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal income = AccountModelValidator.maxIncome;

            // Act
            bool result = accountModelValidator.ValidateIncome(income);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidIncome_WhenMaxIncomeSurpassedError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal income = AccountModelValidator.maxIncome + 0.01m;

            // Act
            accountModelValidator.ValidateIncome(income);
            bool result = accountModelValidator.MaxIncomeSurpassedError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidIncome_WhenNegativeOrZeroError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal income = 0;

            // Act
            accountModelValidator.ValidateIncome(income);
            bool result = accountModelValidator.NegativeOrZeroError;

            // Assert
            Assert.True(result);
        }
        #endregion
        #region IsValidOutcome
        [Fact]
        public void IsValidOutcome_WhenValidOutcome_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal outcome = AccountModelValidator.maxOutcome;
            decimal money = AccountModelValidator.maxOutcome;

            // Act
            bool result = accountModelValidator.ValidateOutcome(outcome, money);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidOutcome_WhenMaxOutcomeSurpassedError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal outcome = AccountModelValidator.maxOutcome + 0.01m;
            decimal money = 0;

            // Act
            accountModelValidator.ValidateOutcome(outcome, money);
            bool result = accountModelValidator.MaxOutcomeSurpassedError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidOutcome_WhenOutcomeLeavesAccountOnRedError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal outcome = 101;
            decimal money = AccountModelValidator.maxDebtAllowed + 100; //100 is the amount of money allowed to withdraw

            // Act
            accountModelValidator.ValidateOutcome(outcome, money);
            bool result = accountModelValidator.OutcomeLeavesAccountOnRedError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidOutcome_WhenNegativeOrZeroError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            decimal outcome = -0;
            decimal money = 0;

            // Act
            accountModelValidator.ValidateOutcome(outcome, money);
            bool result = accountModelValidator.NegativeOrZeroError;

            // Assert
            Assert.True(result);
        }
        #endregion
        #region IsValidNewAccount
        [Fact]
        public void IsValidNewAccount_WhenValidNewAccount_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IBAN0123456789", "1234");

            //Act
            bool result = accountModelValidator.ValidateNewAccount(accountModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidNewAccount_WhenIBANMustBeginWithIBANError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IVAN0123456789", "1234");

            //Act
            accountModelValidator.ValidateNewAccount(accountModel);
            bool result = accountModelValidator.IBANMustBeginWithIBANError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidNewAccount_WhenIBANLengthError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IBAN01234567899679243649262986746239658586870lol", "1234");

            //Act
            accountModelValidator.ValidateNewAccount(accountModel);
            bool result = accountModelValidator.IBANLengthError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidNewAccount_WhenPinMustBeDigitsError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IBAN0123456789", "1_34");

            //Act
            accountModelValidator.ValidateNewAccount(accountModel);
            bool result = accountModelValidator.PinMustBe4DigitsError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidNewAccount_WhenPinMustHaveALengthOf4Error_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IBAN0123456789", "123456");

            //Act
            accountModelValidator.ValidateNewAccount(accountModel);
            bool result = accountModelValidator.PinMustBe4DigitsError;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidNewAccount_WhenIBANMustContainNumbersAfterIBANError_ReturnsTrue()
        {
            // Arrange
            AccountModelValidator accountModelValidator = new();
            AccountModel accountModel = new(0, new(), "IBAN012345camela6789", "1234");

            //Act
            accountModelValidator.ValidateNewAccount(accountModel);
            bool result = accountModelValidator.IBANMustContainNumbersAfterIBANError;

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}