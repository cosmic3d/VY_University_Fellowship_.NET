using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu;

namespace Bank
{
    public class SpanishBankMenu : Menu.Menu
    {
        private SpanishBank Bank { get; }
        private SpanishClient? ClientSession { get; set; }
        public SpanishBankMenu(SpanishBank bank)
        {
            Bank = bank;
            AddMethod(1, "Money Income", MoneyIncome);
            AddMethod(2, "Money Outcome", MoneyOutcome);
            AddMethod(3, "List all movements", ListAllMovements);
            AddMethod(4, "List all incomes", ListIncomes);
            AddMethod(5, "List all outcomes", ListOutcomes);
            AddMethod(6, "Show current money", ShowCurrentMoney);
            AddMethod(7, "Exit", ExitProgram);
            banner = @"
░▒▓███████▓▒░  ░▒▓██████▓▒░ ░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓████████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓███████▓▒░  
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
                                                         
                                                         
";
            BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void AccessMenu()
        {
            CheckCredentials();
            RunMenu();
        }

        private void MoneyIncome()
        {
            Console.Write("Insert the amount of money income: ");
            string? money_income = Console.ReadLine()?.Replace(".", ",").Trim();
            decimal n_money_income = 0m;
            bool success_income = decimal.TryParse(money_income, out n_money_income);
            if (success_income &&
                n_money_income > 0 &&
                n_money_income <= ClientSession?.Bank.GetMaxIncome() &&
                n_money_income >= ClientSession?.Bank.GetMinIncome()
               )
            {
                ClientSession?.AddIncome(n_money_income);
                Console.WriteLine($"You have succesfully added {n_money_income:0.00}{Bank.GetCoin()} to your account");
                ClientSession?.ShowBalance();
            }
            else
            {
                Console.Write("ERROR: ");
                if (!success_income)
                {
                    Console.WriteLine("Invalid input format");
                }
                else if (n_money_income <= 0)
                {
                    Console.WriteLine("The amount of money must be greater than 0");
                }
                else if (n_money_income > ClientSession?.Bank.GetMaxIncome())
                {
                    Console.WriteLine($"The amount of money must be less than {ClientSession?.Bank.GetMaxIncome()}{Bank.GetCoin()}");
                }
                else if (n_money_income < ClientSession?.Bank.GetMinIncome())
                {
                    Console.WriteLine($"The amount of money must be greater than {ClientSession?.Bank.GetMinIncome()}{Bank.GetCoin()}");
                }
            }
        }
        private void MoneyOutcome()
        {
            Console.Write("Insert the amount of money outcome: ");
            string? money_outcome = Console.ReadLine()?.Replace(".", ",").Trim();
            decimal n_money_outcome = 0;
            bool success_outcome = decimal.TryParse(money_outcome, out n_money_outcome);
            if (success_outcome &&
                ClientSession?.Account?.GetBalance() >= n_money_outcome &&
                n_money_outcome <= ClientSession?.Bank.GetMaxOutcome() &&
                n_money_outcome >= ClientSession?.Bank.GetMinOutcome()
               )
            {
                ClientSession?.AddOutcome(n_money_outcome);
                Console.WriteLine($"You have succesfully withdrawn {n_money_outcome:0.00}{Bank.GetCoin()} to your account");
                ClientSession?.ShowBalance();
            }
            else
            {
                Console.Write("ERROR: ");
                if (!success_outcome)
                {
                    Console.WriteLine("Invalid input format");
                }
                else if (ClientSession?.Account?.GetBalance() < n_money_outcome)
                {
                    Console.WriteLine("You don't have enough money in your account");
                    ClientSession?.ShowBalance();
                }
                else if (n_money_outcome > ClientSession?.Bank.GetMaxOutcome())
                {
                    Console.WriteLine($"The amount of money must be less than {ClientSession?.Bank.GetMaxOutcome()}{Bank.GetCoin()}");
                }
                else if (n_money_outcome < ClientSession?.Bank.GetMinOutcome())
                {
                    Console.WriteLine($"The amount of money must be greater than {ClientSession?.Bank.GetMinOutcome()}{Bank.GetCoin()}");
                }
            }
        }


        private void ListAllMovements()
        {
            ClientSession?.ShowTransactions();
        }

        private void ListIncomes()
        {
            ClientSession?.ShowIncome();
        }


        private void ListOutcomes()
        {
            ClientSession?.ShowOutcome();
        }

        private void ShowCurrentMoney()
        {
            ClientSession?.ShowBalance();
        }
        public override void ExitProgram()
        {
            Console.WriteLine("\nGoodbye!");
            ShowCurrentMoney();
        }

        private void CheckCredentials()
        {
            SpanishAccount? account = null;
            string? account_pin;
            do
            {
                Console.Write("Insert your account number: ");
                string? account_number = Console.ReadLine()?.Trim();

                if (account_number == null)
                {
                    Console.WriteLine("An error occurred during the reading of a line");
                }
                else
                {
                    account = Bank.GetClientByIBAN(account_number)?.Account;

                    if (account == null)
                    {
                        Console.WriteLine("The account number does not exist.");
                    }
                    else
                    {
                        do
                        {
                            Console.Write("Insert your account pin: ");
                            account_pin = Console.ReadLine()?.Trim();

                            if (account_pin == null)
                            {
                                Console.WriteLine("An error occurred during the reading of a line");
                            }
                            else
                            {
                                if (account_pin == account.GetAccountPin())
                                {
                                    Console.WriteLine("Account pin correct");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Account pin incorrect");
                                }
                            }
                        } while (true);
                    }
                }

            } while (account == null);
            ClientSession = account.GetClient();
        }
    }
}
