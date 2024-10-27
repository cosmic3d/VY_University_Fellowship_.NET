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
            int grandpaId = GetInt("Enter Grandpa Id: ");
            string? grandpaName = GetString("Enter Grandpa Name: ");
            int grandpaMoney = GetInt("Enter Grandpa Money: ");

            int fatherId = GetInt("Enter Father Id: ");
            string? fatherName = GetString("Enter Father Name: ");
            int fatherMoney = GetInt("Enter Father Money: ");

            int sonId = GetInt("Enter Son Id: ");
            string? sonName = GetString("Enter Son Name: ");
            int sonMoney = GetInt("Enter Son Money: ");
            Son.SetFamilyFields(grandpaId, grandpaName, grandpaMoney, fatherId, fatherName, fatherMoney, sonId, sonName, sonMoney);
        }

        protected override void ExitProgram()
        {
            Console.WriteLine("Exiting Family program...");
        }
    }
}
