namespace BankAccountOOPMultiuser.Infrastructure.Contracts.Entities
{
    public class AccountEntity
    {
        public uint Id;
        public int Balance;
        public List<Tuple<DateTime, int>> Movements;
        public string Pin;
        public string IBAN;
    }
}
