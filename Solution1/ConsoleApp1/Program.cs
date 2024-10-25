// See https://aka.ms/new-console-template for more information
using ClassLibrary1;
using ClassLibrary2;

Console.WriteLine("Hello, World!");

Class11 class11 = new(11);

Class222 class222 = new(222);

String str = (class11.valor + class222.valor).ToString();

Console.WriteLine("El valor es: " + str);