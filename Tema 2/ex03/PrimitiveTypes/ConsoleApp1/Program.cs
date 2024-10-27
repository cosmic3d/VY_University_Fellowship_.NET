#region Variables
bool parseSuccess = true;
bool BoolValue;
int IntValue;
decimal DecimalValue;
char CharValue;
string? StringValue;
DateTime DateTimeValue;
#endregion
#region Asking for input
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me a boolean value: ");
    parseSuccess = bool.TryParse(Console.ReadLine()?.Trim().ToLower(), out BoolValue);
} while (!parseSuccess);
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me an integer value: ");
    parseSuccess = int.TryParse(Console.ReadLine()?.Trim(), out IntValue);
} while (!parseSuccess);
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me a decimal value: ");
    parseSuccess = decimal.TryParse(Console.ReadLine()?.Replace(".", ",").Trim(), out DecimalValue);
} while (!parseSuccess);
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me a character value: ");
    parseSuccess = char.TryParse(Console.ReadLine(), out CharValue);
} while (!parseSuccess);
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me a string value: ");
    StringValue = Console.ReadLine();
    parseSuccess = StringValue != null;
} while (!parseSuccess);
do
{
    if (!parseSuccess)
    {
        Console.WriteLine("An error occurred trying to parse your input.");
    }
    Console.Write("Give me a date and time value: ");
    parseSuccess = DateTime.TryParse(Console.ReadLine()?.Trim(), out DateTimeValue);
} while (!parseSuccess);
#endregion
#region Output
Console.WriteLine($"Negated bool: {!BoolValue}");
try
{
    Console.WriteLine($"Integer divided by decimal: {(Decimal)IntValue / DecimalValue}");
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred trying to divide the integer by the decimal: {e.Message}");
}
StringValue = CharValue + "(" + StringValue + ")" + CharValue;
Console.WriteLine($"Text between parenthesis, preceeded and succeded by the character: {StringValue}");
DateTimeValue = new DateTime(DateTimeValue.Year, DateTimeValue.Month, DateTime.DaysInMonth(DateTimeValue.Year, DateTimeValue.Month), 23, 59, 59);
Console.WriteLine($"Last second from the last day of the month of the given date: {DateTimeValue}");
#endregion