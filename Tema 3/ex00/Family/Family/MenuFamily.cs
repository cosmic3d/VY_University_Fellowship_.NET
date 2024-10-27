using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyClass;

namespace FamilyConsoleApp
{
    internal class MenuFamily : Menu.Menu
    {
        public ConsoleKey ContinueKey = ConsoleKey.Enter, ExitKey = ConsoleKey.Escape;
        private Son Son;

        public MenuFamily(Son son)
        {
            Son = son;
            AddMethod(1, "Show Values", ShowFamilyFields);
            AddMethod(2, "Modify Values", SetFamilyFields);
            AddMethod(3, "Exit Program", ExitProgram);

        }

        public void RunMenuFamily()
        {
            int option;
            bool success;
            do
            {
                Console.Clear();
                ShowMenu();
                Console.Write("\nChoose an option: ");
                success = int.TryParse(Console.ReadLine()?.Trim(), out option);
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
                    break;
                }

            } while (true);
        }
        private void ShowFamilyFields()
        {
            Son.ShowFamilyFields();
        }

        private void SetFamilyFields()
        {
            Console.WriteLine("Enter Grandpa Id:");
            int grandpaId = GetInt();
            string? grandpaName;
            do
            {
                Console.WriteLine("Enter Grandpa Name:");
                grandpaName = GetString();
            } while (string.IsNullOrEmpty(grandpaName));

            Console.WriteLine("Enter Grandpa Money:");
            int grandpaMoney = GetInt();
            Console.WriteLine("Enter Father Id:");
            int fatherId = GetInt();
            string? fatherName;
            do
            {
                Console.WriteLine("Enter Father Name:");
                fatherName = GetString();
            } while (string.IsNullOrEmpty(fatherName));

            Console.WriteLine("Enter Father Money:");
            int fatherMoney = GetInt();
            Console.WriteLine("Enter Son Id:");
            int sonId = GetInt();
            string? sonName;
            do
            {
                Console.WriteLine("Enter Son Name:");
                sonName = GetString();
            } while (string.IsNullOrEmpty(sonName));

            Console.WriteLine("Enter Son Money:");
            int sonMoney = GetInt();
            Son.SetFamilyFields(grandpaId, grandpaName, grandpaMoney, fatherId, fatherName, fatherMoney, sonId, sonName, sonMoney);
        }

        private void ExitProgram()
        {
            Console.WriteLine("Exiting Family program...");
        }

        private int GetInt()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return number;
        }

        private string? GetString()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please enter a string.");
                }
                else
                {
                    return input;
                }
            }
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
    }
}
