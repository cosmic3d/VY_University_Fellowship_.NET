using System;
using System.Collections.Generic;

namespace Menu
{
    public abstract class Menu
    {
        protected internal string banner = "Welcome to this Menu!";
        protected internal ConsoleColor BackgroundColor = ConsoleColor.Black;
        protected internal ConsoleColor FontColor = ConsoleColor.White;
        public ConsoleKey ContinueKey = ConsoleKey.Enter, ExitKey = ConsoleKey.Escape;
        protected List<MenuMethod> Methods { get; }

        protected Menu()
        {
            Methods = new List<MenuMethod>();
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

        internal protected virtual void ExitProgram()
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

        internal protected void AddMethod(int id, string description, Action method)
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

        protected int GetInt(string requirement)
        {
            Console.Write(requirement + ": ");
            int number;
            while (!int.TryParse(Console.ReadLine()?.Trim(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write(requirement + ": ");
            }
            return number;
        }

        protected uint GetUint(string requirement)
        {
            Console.Write(requirement + ": ");
            uint number;
            while (!uint.TryParse(Console.ReadLine()?.Trim(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                Console.Write(requirement + ": ");
            }
            return number;
        }

        protected string? GetString(string requirement)
        {
            while (true)
            {
                Console.Write(requirement + ": ");
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

        protected DateTime GetDateTime(string requirement)
        {
            Console.Write(requirement + ": ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine()?.Trim(), out date))
            {
                Console.WriteLine("Invalid input. Please enter a date.");
                Console.Write(requirement + ": ");
            }
            return date;
        }
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
