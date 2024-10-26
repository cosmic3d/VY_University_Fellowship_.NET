using System;
using System.Collections.Generic;

namespace Menu
{
    public abstract class Menu
    {
        protected List<MenuMethod> Methods { get; }

        protected Menu()
        {
            Methods = new List<MenuMethod>();
        }

        protected void AddMethod(int id, string description, Action method)
        {
            Methods.Add(new MenuMethod(id, description, method));
        }

        public int GetMethodId(Action action)
        {
            MenuMethod? menuMethod = Methods.Find(m => m.Method == action);
            return menuMethod?.Id ?? -1;
        }

        public void ShowMenu()
        {
            foreach (var method in Methods)
            {
                Console.WriteLine($"{method.Id}: {method.Description}");
            }
        }
        public void ExecuteMethod(int id)
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
