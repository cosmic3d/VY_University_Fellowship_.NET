
using BankAccountOOPMultiuser.Business.Impl;
using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using Moq;
using BankAccountOOPMultiuser.Business.Contracts.DTOs;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Testing.UnitTesting.Business
{
    public class AccountServiceUnitTests
    {
        #region GetAccounts
        [Fact]
        public void GetAccounts_WhenThereAreAccounts_ReturnsAccounts()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            _mockAccountRepository
                .Setup(x => x.GetAllAccounts()).Returns(new List<AccountEntity>()
                {
                    new()
                });

            AccountService sut = new(_mockAccountRepository.Object);

            // Act
            List<FullAccountDto>? result = sut.GetAccounts();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAccounts_WhenAccountsIsNull_ReturnsNull()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            // Act
            List<FullAccountDto>? result = sut.GetAccounts();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAccounts_WhenAccountsCountIsZero_ReturnsNull()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            _mockAccountRepository
                .Setup(x => x.GetAllAccounts()).Returns(new List<AccountEntity>());

            AccountService sut = new(_mockAccountRepository.Object);

            // Act
            List<FullAccountDto>? result = sut.GetAccounts();

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region GetAccount
        [Fact]
        public void GetAccount_WhenThereIsAccount_ReturnsAccount()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            _mockAccountRepository
                .Setup(x => x.GetAccountFromRepository(It.IsAny<string>())).Returns(new AccountEntity());

            AccountService sut = new(_mockAccountRepository.Object);

            // Act
            FullAccountDto? result = sut.GetAccount(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAccount_WhenAccountIsNull_ReturnsNull()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            // Act
            FullAccountDto? result = sut.GetAccount(It.IsAny<string>());

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region AddAccount
        [Fact]
        public void AddAccount_WhenValidInput_ReturnsNoErrors()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new()
            {
                Iban = "IBAN018429482",
                Pin = "1234"
            };
            // Act

            FullAccountDto? result = sut.AddAccount(newAccountDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result!.HasErrors);
        }
        [Fact]
        public void AddAccount_WhenAccountAlreadyExists_ReturnAccountAlreadyExistsError()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();
            _mockAccountRepository.
                Setup(x => x.GetAccountFromRepository(It.IsAny<string>())).Returns(new AccountEntity());

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new();
            // Act

            FullAccountDto? result = sut.AddAccount(newAccountDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result!.HasErrors);
            Assert.Equal(AccountErrorEnum.AccountAlreadyExistsError, result.AccountError);
        }

        [Fact]
        public void AddAccount_WhenIBANMustBeginWithIBAN_ReturnIBANMustBeginWithIBANError()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new()
            {
                Iban = "IVAN9479278",
                Pin = "1234"
            };
            // Act

            FullAccountDto? result = sut.AddAccount(newAccountDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result!.HasErrors);
            Assert.Equal(AccountErrorEnum.IBANMustBeginWithIBANError, result.AccountError);
        }

        [Fact]
        public void AddAccount_WhenIBANLengthInvalid_ReturnIBANLengthInvalid()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new()
            {
                Iban = "IBAN94792796732067027605760270602496795589785759785908",
                Pin = "1234"
            };
            // Act

            FullAccountDto? result = sut.AddAccount(newAccountDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result!.HasErrors);
            Assert.Equal(AccountErrorEnum.IBANLengthInvalidError, result.AccountError);
        }

        [Fact]
        public void AddAccount_WhenPinMustHaveALengthOf4_ReturnPinMustBe4DigitsError()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new()
            {
                Iban = "IBAN01928394",
                Pin = "10394"
            };

            // Act
            FullAccountDto? result = sut.AddAccount(newAccountDto);

            Assert.NotNull(result);
            Assert.True(result!.HasErrors);
            Assert.Equal(AccountErrorEnum.PinMustBe4DigitsError, result.AccountError);
        }

        [Fact]
        public void AddAccount_WhenPinMustOnlyContainDigits_ReturnPinMustBe4DigitsError()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new()
            {
                Iban = "IBAN01928394",
                Pin = "1A39"
            };

            // Act
            FullAccountDto? result = sut.AddAccount(newAccountDto);

            Assert.NotNull(result);
            Assert.True(result!.HasErrors);
            Assert.Equal(AccountErrorEnum.PinMustBe4DigitsError, result.AccountError);
        }
        #endregion
    }
}
