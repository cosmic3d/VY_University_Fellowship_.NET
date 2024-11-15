﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class Client<TAccount, TBank>
    {
        private static int Id_idx = 0;

        public int Id { get; }
        internal TBank ClientBank { get; }
        internal TAccount? ClientAccount { get; }

        protected Client(TBank bank)
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

        protected abstract TAccount CreateAccount();
    }



    public class SpanishClient : Client<SpanishAccount, SpanishBank>
    {
        internal SpanishBank Bank { get; }
        internal SpanishAccount? Account { get; }

        public SpanishClient(SpanishBank bank) : base(bank)
        {
            Bank = bank;
            Account = CreateAccount(); //Llamada al método de Client y cast a SpanishAccount para que se llame a los métodos específicos
        }

        public override void ShowBalance()
        {
            Account?.ShowBalance();
        }

        public override void AddIncome(decimal income)
        {
            Account?.AddIncome(income);
        }

        public override void AddOutcome(decimal outcome)
        {
            Account?.AddOutcome(outcome);
        }

        public override void ShowTransactions()
        {
            Account?.ShowTransactions();
        }

        public override void ShowIncome()
        {
            Account?.ShowIncome();
        }

        public override void ShowOutcome()
        {
            Account?.ShowOutcome();
        }

        public override string GetClientInfo()
        {
            string str = "";
            str += $"Client ID: {Id}\n";
            str += $"Bank: {Bank.Name}\n";
            str += $"Account number: {Account?.GetAccountNumber()} \n";
            str += $"Account pin: {Account?.GetAccountPin()} \n\n";
            return str;
        }
        protected override SpanishAccount CreateAccount()
        {
            return new SpanishAccount(this);
        }
    }
}
