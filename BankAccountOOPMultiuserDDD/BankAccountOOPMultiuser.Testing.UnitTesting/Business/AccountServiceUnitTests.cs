
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
        #region AddAccount
        [Fact]
        public void AddAccount_WhenValidInput_ReturnsNoErrors()
        {
            // Arrange
            Mock<IAccountRepository> _mockAccountRepository = new();

            AccountService sut = new(_mockAccountRepository.Object);

            NewAccountDto newAccountDto = new NewAccountDto()
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

            NewAccountDto newAccountDto = new NewAccountDto();
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

            NewAccountDto newAccountDto = new NewAccountDto()
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
        #endregion
    }
}
