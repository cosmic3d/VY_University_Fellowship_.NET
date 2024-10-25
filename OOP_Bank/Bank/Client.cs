using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class Client
    {
        private static int Id_idx = 0;

        public int Id { get; }
        internal Bank ClientBank { get; }
        internal Account? ClientAccount { get; }

        protected Client(Bank bank)
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

        //public method for printing client information
        abstract public void ShowClientInfo();

        protected abstract Account CreateAccount();
    }

    public class SpanishClient : Client
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

        public override void ShowClientInfo()
        {
            Console.WriteLine($"Client ID: {Id}");
            Console.WriteLine($"Bank: {Bank.Name}");
            Console.WriteLine($"Account number: {Account.GetAccountNumber()}");
            Console.WriteLine($"Account pin: {Account.GetAccountPin()}");
        }

        // Implementación del método abstracto para la cuenta
        protected override SpanishAccount CreateAccount()
        {
            return new SpanishAccount(this);
        }
    }
}
