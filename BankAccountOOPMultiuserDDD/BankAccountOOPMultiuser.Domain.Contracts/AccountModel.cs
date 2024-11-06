namespace BankAccountOOPMultiuser.Domain.Models
{
    public class AccountModel
    {
        public decimal Balance;
        public List<Tuple<DateTime, decimal>> Movements;
        public readonly string Iban;
        public readonly string Pin;

        public AccountModel(decimal balance, List<Tuple<DateTime, decimal>> movements,string iban, string pin)
        {
            this.Balance = balance;
            this.Movements = movements;
            this.Iban = iban;
            this.Pin = pin;
        }

        public void AddIncome(decimal amount)
        {
            Balance += amount;
            Movements.Add(new(DateTime.Now, amount));
        }

        public void AddOutcome(decimal amount)
        {
            Balance -= amount;
            Movements.Add(new(DateTime.Now, -amount));
        }

        public bool CorrectPin(string pin) => Pin == pin;

        public List<Tuple<DateTime, decimal>>? GetAllMovements() => Movements.Any() ? Movements.ToList() : null;

        public List<Tuple<DateTime, decimal>>? GetAllIncomes()
        {
            if (Movements.Count == 0) return null;
            var incomes = Movements.Where(x => x.Item2 > 0);
            return incomes.Any() ? incomes.ToList() : null;
        }
        public List<Tuple<DateTime, decimal>>? GetAllOutcomes()
        {
            if (Movements.Count == 0) return null;
            var outcomes = Movements.Where(x => x.Item2 < 0);
            return outcomes.Any() ? outcomes.ToList() : null;
        }

        public decimal GetBalance() => Balance;
    }
}
