namespace BankAccountOOPMultiuser.XCutting.Enums
{
    public enum AccountErrorEnum
    {
        AccountAlreadyExistsError,
        IBANMustBeginWithIBANError,
        IBANLengthInvalidError,
        PinMustBe4DigitsError,
        AccountNotFound
    }
}
