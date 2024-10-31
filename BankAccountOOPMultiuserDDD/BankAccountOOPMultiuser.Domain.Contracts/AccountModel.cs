namespace BankAccountOOPMultiuser.Domain.Contracts
{
    public class AccountModel
    {
        public int Balance;
        public List<Tuple<DateTime, int>> Movements;
        public string Pin;

        public AccountModel(int balance, List<Tuple<DateTime, int>> movements, string pin)
        {
            this.Balance = balance;
            this.Movements = movements;
            this.Pin = pin;
        }

        public void AddIncome(int amount)
        { 
            Balance += amount;
            Movements.Add(new(DateTime.Now, amount));
        }

        public void AddOutcome(int amount)
        {
            Balance -= amount;
            Movements.Add(new(DateTime.Now, -amount));
        }

    }
}
