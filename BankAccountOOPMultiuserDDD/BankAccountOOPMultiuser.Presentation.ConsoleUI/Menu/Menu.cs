﻿using System.Runtime.InteropServices;

namespace BankAccountOOPMultiuser.Presentation.ConsoleUI.Menu
{
    public abstract class Menu
    {
        protected internal string? welcome = "Welcome to the program!";
        protected internal string banner = "";
        protected internal ConsoleColor BackgroundColor = ConsoleColor.Black;
        protected internal ConsoleColor FontColor = ConsoleColor.White;
        public ConsoleKey ContinueKey = ConsoleKey.Enter, ExitKey = ConsoleKey.Escape;
        protected List<MenuMethod> Methods { get; }

        protected Menu()
        {
            Methods = new List<MenuMethod>();
        }

        public virtual void RunMenu()
        {
            int option;
            bool success;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Beep(1500, 500);
            }
            do
            {
                ColorBanner();
                ShowMenu();
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
                    ExitProgram();
                    break;
                }

            } while (true);
        }

        public virtual void ExitProgram()
        {
            Console.WriteLine("Goodbye!");
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

        internal protected virtual void ColorBanner()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = FontColor;
            Console.Clear();
            Console.WriteLine(banner);
        }

        public virtual void AddMethod(int id, string description, Action method)
        {
            Methods.Add(new MenuMethod(id, description, method));
        }

        internal protected int GetMethodId(Action action)
        {
            MenuMethod? menuMethod = Methods.Find(m => m.Method == action);
            return menuMethod?.Id ?? -1;
        }

        public virtual void ShowMenu()
        {
            foreach (var method in Methods)
            {
                Console.WriteLine($"{method.Id}: {method.Description}");
            }
            if (welcome != null)
            {
                Console.WriteLine(welcome);
                welcome = null;
            }
            Console.Write("\nChoose an option: ");
        }
        internal protected void ExecuteMethod(int id)
        {
            var menuMethod = Methods.Find(m => m.Id == id);
            if (menuMethod != null)
            {
                menuMethod.Method();
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }

        public int GetMenuLength() => Methods.Count;
    }

    public class MenuMethod
    {
        public int Id { get; }
        public string Description { get; }
        public Action Method { get; } // Action sirve para definir un método sin parámetros y sin retorno

        public MenuMethod(int id, string description, Action method)
        {
            Id = id;
            Description = description;
            Method = method;
        }
    }
}
