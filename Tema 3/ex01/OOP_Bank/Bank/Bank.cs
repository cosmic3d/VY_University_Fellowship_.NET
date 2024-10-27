namespace Bank
{
    public abstract class Bank<TClient> where TClient : class
    {
        public static int AccountNumber_idx = 0;
        protected Random Random = new(DateTime.Now.Millisecond);

        public abstract string Name { get; set; }
        protected abstract int MaxIncome { get; set; }
        protected abstract int MinIncome { get; set; }
        protected abstract int MaxOutcome { get; set; }
        protected abstract int MinOutcome { get; set; }
        protected abstract char Coin { get; }
        protected abstract List<TClient> Clients { get; }

        public abstract void AddClient();

        public abstract int GetMinOutcome();
        public abstract int GetMaxOutcome();
        public abstract int GetMinIncome();
        public abstract int GetMaxIncome();
        public abstract char GetCoin();
        public abstract string GetName();

        public int GetAccountNumberIdx() => AccountNumber_idx++;
    }

    public class SpanishBank : Bank<SpanishClient>
    {
        private static int IBAN_idx = 0;

        public override string Name { get; set; }
        protected override int MaxIncome { get; set; }
        protected override int MinIncome { get; set; }
        protected override int MaxOutcome { get; set; }
        protected override int MinOutcome { get; set; }
        protected override char Coin { get; }
        protected override List<SpanishClient> Clients { get; } = new List<SpanishClient>();

        public string CodigoEntidadFinanciera { get; }
        public string CodigoOficinaFinanciera { get; }

        public SpanishBank()
        {
            Name = "Spanish Bank";
            MaxIncome = 20000;
            MinIncome = 5;
            MaxOutcome = 3000;
            MinOutcome = 5;
            Coin = '€';
            CodigoEntidadFinanciera = "1234";
            CodigoOficinaFinanciera = "5678";
        }

        public SpanishBank(string name, string codigoEntidadFinanciera, string codigoOficinaFinanciera, int maxIncome, int minIncome, int maxOutcome, int minOutcome)
        {
            Name = name;
            MaxIncome = maxIncome;
            MinIncome = minIncome;
            MaxOutcome = maxOutcome;
            MinOutcome = minOutcome;
            Coin = '€';
            CodigoEntidadFinanciera = Check4DigitNumber(codigoEntidadFinanciera);
            CodigoOficinaFinanciera = Check4DigitNumber(codigoOficinaFinanciera);
        }

        public string GetIBAN()
        {
            int controlCode = Random.Next(0, 100);

            return $"ES{controlCode:D2}{CodigoEntidadFinanciera}{CodigoOficinaFinanciera}{controlCode:D2}{IBAN_idx++:D10}";
        }

        public override void AddClient() => Clients.Add(new SpanishClient(this));

        public override int GetMinOutcome() => MinOutcome;
        public override int GetMaxOutcome() => MaxOutcome;
        public override int GetMinIncome() => MinIncome;
        public override int GetMaxIncome() => MaxIncome;
        public override string GetName() => Name;
        public override char GetCoin() => Coin;


        //get client by id
        public SpanishClient? GetClientById(int id)
        {
            foreach (SpanishClient client in Clients)
            {
                if (client.Id == id)
                {
                    return client;
                }
            }
            return null;
        }

        //get client by IBAN
        public SpanishClient? GetClientByIBAN(string IBAN) {
            foreach (SpanishClient client in Clients)
            {
                if (client.Account?.GetAccountNumber() == IBAN)
                {
                    return client;
                }
            }
            return null;
        }

        private string Check4DigitNumber(string str)
        {
            if (str.Length == 4 && int.TryParse(str, out _))
            {
                return str;
            }
            else
            {
                throw new ArgumentException("The entity code must be a 4-digit number");
            }
        }
    }


}
