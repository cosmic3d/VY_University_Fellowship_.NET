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
        private Son Son;

        public MenuFamily(Son son)
        {
            Son = son;
            AddMethod(1, "Show Values", ShowFamilyFields);
            AddMethod(2, "Modify Values", SetFamilyFields);
            AddMethod(3, "Exit Program", ExitProgram);
            banner = @" ___           _ _      
| __|_ _ _ __ (_) |_  _ 
| _/ _` | '  \| | | || |
|_|\__,_|_|_|_|_|_|\_, |
                   |__/ ";
            BackgroundColor = ConsoleColor.DarkMagenta;

        }

        public void RunMenuFamily()
        {
            RunMenu();
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

        protected override void ExitProgram()
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
    }
}
