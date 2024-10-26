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
        public ConsoleKey ContinueKey = ConsoleKey.Enter, ExitKey = ConsoleKey.Escape;
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
        }
        public void AccessMenu()
        {
            CheckCredentials();
            RunMenu();
        }

        private void MoneyIncome()
        {
            Console.Write("Insert the amount of money income: ");
            string? money_income = Console.ReadLine()?.Replace(".", ",");
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
            string? money_outcome = Console.ReadLine()?.Replace(".", ",");
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
        private void ExitProgram()
        {
            return;
        }

        public void RunMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int option;
            bool success;
            do
            {
                ColorBanner();
                ShowMenu();
                Console.Write("\nChoose an option: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (success)
                {
                    if (option == GetMethodId(ExitProgram))
                        break;
                    ExecuteMethod(option);
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
                ConsoleKeyInfo choice = InputChoice(ContinueKey, ExitKey);
                if (choice.Key == ExitKey)
                {
                    ClientSession?.ShowBalance();
                    break;
                }

            } while (true);
        }

        private ConsoleKeyInfo InputChoice(ConsoleKey continue_key, ConsoleKey exit_key)
        {
            ConsoleKeyInfo consoleKey = new();
            Console.WriteLine($"Use {continue_key.ToString().ToUpper()} to continue");
            Console.Write($"Use {exit_key.ToString().ToUpper()} to exit the program");
            do
            {
                consoleKey = Console.ReadKey(true); // true to prevent keys from being shown
            } while (consoleKey.Key != continue_key && consoleKey.Key != exit_key);

            return consoleKey;
        }

        private void ColorBanner()
        {
            const string banner = @"
░▒▓███████▓▒░  ░▒▓██████▓▒░ ░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓████████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓███████▓▒░  
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
                                                         
                                                         
";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine(banner);
        }

        private void CheckCredentials()
        {
            SpanishAccount? account = null;
            string? account_pin;
            do
            {
                Console.Write("Insert your account number: ");
                string? account_number = Console.ReadLine();

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
                            account_pin = Console.ReadLine();

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
