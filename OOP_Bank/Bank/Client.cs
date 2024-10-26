using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class Client<T> where T : class
    {
        private static int Id_idx = 0;

        public int Id { get; }
        internal Bank<Client<T>> ClientBank { get; }
        internal T? ClientAccount { get; }

        protected Client(Bank<Client<T>> bank)
        {
            Id = Id_idx++;
            ClientBank = bank;
        }

        public abstract void ShowBalance();
        public abstract void AddIncome(decimal income);
        public abstract void AddOutcome(decimal outcome);
        public abstract void ShowTransactions();
        public abstract void ShowIncome();
        public abstract void ShowOutcome();
        public abstract string GetClientInfo();

        protected abstract T CreateAccount();
    }


    public class SpanishClient : Client<SpanishAccount>
    {
        internal SpanishBank Bank { get; }
        internal SpanishAccount Account { get; }

        public SpanishClient(SpanishBank bank) : base(bank)
        {
            Bank = bank;
            Account = CreateAccount(); //Llamada al método de Client y cast a SpanishAccount para que se llame a los métodos específicos
        }

        public override void ShowBalance()
        {
            Console.WriteLine($"Your balance is {Account.GetBalance():0.00}");
        }

        public override void AddIncome(decimal income)
        {
            if (income < 0)
            {
                Console.WriteLine("Income can't be negative");
                return;
            }
            if (income > Bank.GetMaxIncome())
            {
                Console.WriteLine($"Income can't be more than {Bank.GetMaxIncome()}");
                return;
            }
            if (income < Bank.GetMinIncome())
            {
                Console.WriteLine($"Income can't be less than {Bank.GetMinIncome()}");
                return;
            }
            Account.AddIncome(income);
        }

        public override void AddOutcome(decimal outcome)
        {
            if (outcome > Bank.GetMaxOutcome())
            {
                Console.WriteLine($"Outcome can't be more than {Bank.GetMaxOutcome()}");
                return;
            }
            if (outcome < Bank.GetMinOutcome())
            {
                Console.WriteLine($"Outcome can't be less than {Bank.GetMinOutcome()}");
                return;
            }
            Account.AddOutcome(outcome);
        }

        public override void ShowTransactions()
        {
            Account.ShowTransactions();
        }

        public override void ShowIncome()
        {
            Account.ShowIncome();
        }

        public override void ShowOutcome()
        {
            Account.ShowOutcome();
        }

        public override string GetClientInfo()
        {
            string str = "";
            str += $"Client ID: {Id}\n";
            str += $"Bank: {Bank.Name}\n";
            str += $"Account number: {Account.GetAccountNumber()} \n";
            str += $"Account pin: {Account.GetAccountPin()} \n\n";
            return str;
        }
        protected override SpanishAccount CreateAccount()
        {
            return new SpanishAccount(this);
        }
    }
}
