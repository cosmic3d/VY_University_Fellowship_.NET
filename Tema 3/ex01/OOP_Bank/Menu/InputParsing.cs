﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public static class InputParsing
    {
        public static int GetInt(string requirement)
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

        public static uint GetUint(string requirement)
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

        public static string? GetString(string requirement)
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

        public static DateTime GetDateTime(string requirement)
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
}