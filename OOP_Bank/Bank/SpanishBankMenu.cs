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
        private SpanishClient? Client { get; set; }
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
            throw new NotImplementedException();
        }
        private void MoneyOutcome()
        {
            throw new NotImplementedException();
        }


        private void ListAllMovements()
        {
            throw new NotImplementedException();
        }

        private void ListIncomes()
        {
            throw new NotImplementedException();
        }


        private void ListOutcomes()
        {
            throw new NotImplementedException();
        }

        private void ShowCurrentMoney()
        {
            throw new NotImplementedException();
        }
        private void ExitProgram()
        {
            throw new NotImplementedException();
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
                    ExecuteMethod(option);
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
                ConsoleKeyInfo choice = InputChoice(ContinueKey, ExitKey);
                if (choice.Key == ExitKey)
                {

                    break;
                }

            } while (option != GetMethodId(ExitProgram));
        }

        private ConsoleKeyInfo InputChoice(ConsoleKey continue_key, ConsoleKey exit_key)
        {
            ConsoleKeyInfo consoleKey = new();
            Console.WriteLine($"Use {continue_key.ToString().ToUpper()} to continue");
            Console.WriteLine($"Use {exit_key.ToString().ToUpper()} to exit the program");
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
            Client = account.GetClient() as SpanishClient;
        }
    }
}
