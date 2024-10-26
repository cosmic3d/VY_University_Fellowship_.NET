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
        public SpanishBankMenu(SpanishBank bank)
        {
            Bank = bank;
            AddMethod(1, "Show client information", ShowClientInfo);
            AddMethod(2, "Show balance", ShowBalance);
            AddMethod(3, "Add income", AddIncome);
            AddMethod(4, "Add outcome", AddOutcome);
            AddMethod(5, "Show transactions", ShowTransactions);
            AddMethod(6, "Show income", ShowIncome);
            AddMethod(7, "Show outcome", ShowOutcome);
        }

        public void RunMenu()
        {
            CheckCredentials();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int option;
            bool success;
            do
            {
                ColorBanner();
                ShowMenu();
                Console.Write("\nChoose an option: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (!success)
                {
                    ConsoleKeyInfo consoleKey = new();
                    Console.Write("Invalid option. Use ENTER to continue");
                    do
                    {
                        consoleKey = Console.ReadKey();
                    } while (consoleKey.Key != ConsoleKey.Enter);
                }
                ExecuteMethod(option);
            } while (option != GetMenuLength());
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
        }

        private void AccessMenu()
        {
            CheckCredentials();
            RunMenu();
        }

        public void ShowClientInfo()
        {
        }

        public void ShowBalance()
        {
        }

        public void AddIncome()
        {
        }

        public void AddOutcome()
        {
        }

        public void ShowTransactions()
        {
        }

        public void ShowIncome()
        {
        }

        public void ShowOutcome()
        {
        }
    }
}
