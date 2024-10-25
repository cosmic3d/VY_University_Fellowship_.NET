using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class Account
    {
        protected Client Client { get; }
        protected decimal Balance { get; set; }
        protected string AccountNumber { get; set; }
        protected string AccountPin { get; }
        protected List<Tuple<DateTime, decimal>> Transactions { get; }

        protected Account(Client client)
        {
            Client = client;
            Balance = 0;
            Transactions = new List<Tuple<DateTime, decimal>>();
            AccountPin = GeneratePin(); // PIN aleatorio
            AccountNumber = $"{client.ClientBank.GetAccountNumberIdx().ToString():D10}";
        }

        public abstract void AddIncome(decimal income);
        public abstract void AddOutcome(decimal outcome);
        public abstract decimal GetBalance();
        public abstract string GetAccountNumber();
        public abstract string GetAccountPin();
        public abstract void ShowBalance();
        public abstract void ShowTransactions();
        public abstract void ShowIncome();
        public abstract void ShowOutcome();
        protected string GeneratePin()
        {
            Random random = new Random();
            return $"{random.Next(0, 10000):D4}";
        }
    }

    public class SpanishAccount : Account
    {
        public SpanishAccount(SpanishClient client) : base(client)
        {
            AccountNumber = client.Bank.GetIBAN(); // IBAN español
        }

        // Implementación de los métodos abstractos definidos en Account
        public override void AddIncome(decimal income)
        {
            income = Math.Abs(income);
            Balance += income;
            Transactions.Add(new Tuple<DateTime, decimal>(DateTime.Now, income));
        }

        public override void AddOutcome(decimal outcome)
        {
            outcome = Math.Abs(outcome);
            Balance -= outcome;
            Transactions.Add(new Tuple<DateTime, decimal>(DateTime.Now, -outcome));
        }

        public override decimal GetBalance() => Balance;
        public override string GetAccountNumber() => AccountNumber;
        public override string GetAccountPin() => AccountPin;

        public override void ShowBalance()
        {
            Console.WriteLine($"Your balance is {Balance}");
        }

        public override void ShowTransactions()
        {
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"{transaction.Item1} - {transaction.Item2}");
            }
        }

        public override void ShowIncome()
        {
            foreach (var transaction in Transactions)
            {
                if (transaction.Item2 > 0)
                {
                    Console.WriteLine($"{transaction.Item1} - {transaction.Item2}");
                }
            }
        }

        public override void ShowOutcome()
        {
            foreach (var transaction in Transactions)
            {
                if (transaction.Item2 < 0)
                {
                    Console.WriteLine($"{transaction.Item1} - {transaction.Item2}");
                }
            }
        }
    }
}
