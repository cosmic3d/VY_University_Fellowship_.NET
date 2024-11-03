namespace BankAccountOOPMultiuser.Infrastructure.Contracts.Entities
{
    public class AccountEntity
    {
        public uint Id;
        public decimal Balance;
        public List<Tuple<DateTime, decimal>> Movements;
        public string Pin;
        public string IBAN;
    }
}
