#region VARIABLE_DECLARATION

#region COLOR_VARIABLES
const int BG_IDX = 0;
const int FONT_IDX = 1;

ConsoleColor[] COLOR_MENU = { ConsoleColor.Blue, ConsoleColor.White };
ConsoleColor[] COLOR_SUCCESS = { ConsoleColor.Green, ConsoleColor.White };
ConsoleColor[] COLOR_ERROR = { ConsoleColor.Black, ConsoleColor.White };
#endregion

const int MAX_INCOME = 5000;

// Inicializamos la lista con 10 elementos, asignando un valor por defecto a cada posición.
List<int> accounts_money = new List<int>(new int[10]);

// Inicializamos la lista con 10 listas vacías internas.
List<List<int>> account_movements = new List<List<int>>(10);
List<List<DateTime>> account_dateTimes = new List<List<DateTime>>(10);

// Añadimos listas vacías a cada posición de las listas de listas.
for (int i = 0; i < 10; i++)
{
    account_movements.Add(new List<int>());
    account_dateTimes.Add(new List<DateTime>());
}

List<string> account_numbers = new List<string>()
{
    "1234567890", "0987654321", "1122334455", "5566778899",
    "4433221100", "6677889900", "1029384756", "5647382910",
    "9988776655", "2233445566"
};

List<string> account_pins = new List<string>()
{
    "1234", "4321", "1111", "2222",
    "3333", "4444", "5555", "6666",
    "7777", "8888"
};
const char coin = '€';

Console.OutputEncoding = System.Text.Encoding.UTF8;

int n_answer = 0;

string? account_number;
int account_index = -1;
#endregion

#region THEME
Console.BackgroundColor = COLOR_MENU[BG_IDX];
Console.ForegroundColor = COLOR_MENU[FONT_IDX];
Console.Clear();
#endregion

#region BANNER
const string banner = @"
░▒▓███████▓▒░  ░▒▓██████▓▒░ ░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓████████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓███████▓▒░  
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓███████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
                                                         
                                                         
";
Console.WriteLine(banner);
#endregion

#region CREDENTIALS
do
{
    Console.Write("Insert your account number: ");
    account_number = Console.ReadLine();

    if (account_number == null)
    {
        Console.WriteLine("An error occurred during the reading of a line");
    }
    else
    {
        account_index = account_numbers.IndexOf(account_number);

        if (account_index == -1)
        {
            Console.WriteLine("The account number does not exist.");
        }
        else
        {
            do
            {
                Console.Write("Insert your account pin: ");
                string? account_pin = Console.ReadLine();

                if (account_pin == null)
                {
                    Console.WriteLine("An error occurred during the reading of a line");
                }
                else
                {
                    if (account_pin == account_pins[account_index])
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

} while (account_number == null || account_index == -1);
#endregion

#region PROGRAM_LOOP
while (true)
{
    #region THEME
    Console.BackgroundColor = COLOR_MENU[BG_IDX];
    Console.ForegroundColor = COLOR_MENU[FONT_IDX];
    Console.Clear();
    #endregion

    #region BANNER
    Console.WriteLine(banner);
    #endregion

    #region PROGRAM
    Console.WriteLine(@"
    1. Money income
    2. Money outcome
    3. List all movements
    4. List incomes
    5. List outcomes
    6. Show current money
    7. Exit

    ");

    string? answer;

    Console.Write("Choose an option: ");
    answer = Console.ReadLine();

    n_answer = (answer != null && answer.Length > 0 && answer[0] >= '1' && answer[0] <= '7') ? answer[0] - 48 : 0;

    switch (n_answer)
    {
        case 0:
            break;
        case 1:
            Console.Write("Insert the amount of money income: ");
            string? money_income = Console.ReadLine();
            int n_money_income = 0;
            bool success_income = int.TryParse(money_income, out n_money_income);
            if (success_income && n_money_income >= 0 && n_money_income <= MAX_INCOME)
            {
                if (n_money_income != 0)
                {
                    account_movements[account_index].Add(n_money_income);
                    account_dateTimes[account_index].Add(DateTime.Now);
                    if (accounts_money[account_index] + n_money_income > 0)
                    {
                        accounts_money[account_index] += n_money_income;
                    }
                    else
                    {
                        Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                        Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                        Console.WriteLine("You have reached the maximum amount of money");
                        break;
                    }
                    Console.BackgroundColor = COLOR_SUCCESS[BG_IDX];
                    Console.ForegroundColor = COLOR_SUCCESS[FONT_IDX];
                    Console.WriteLine("The operation was succesfully performed");
                }
                else
                {
                    Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                    Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                    Console.WriteLine("Zero is not a valid value. No operation was performed");
                }
            }
            else
            {
                Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                if (!success_income)
                {
                    Console.WriteLine("Incorrect number format. No operation was performed");
                }
                else if (n_money_income < 0)
                {
                    Console.WriteLine("You cannot retrieve a negative amount of money. No operation was performed");
                }
                else
                {
                    Console.WriteLine("Max income is " + MAX_INCOME.ToString() + coin + ". No operation was performed");
                }
            }
            Console.BackgroundColor = COLOR_MENU[BG_IDX];
            Console.ForegroundColor = COLOR_MENU[FONT_IDX];
            break;
        case 2:
            Console.Write("Insert the amount of money outcome: ");
            string? money_outcome = Console.ReadLine();
            int n_money_outcome = 0;
            bool success_outcome = int.TryParse(money_outcome, out n_money_outcome);
            if (success_outcome)
            {
                n_money_outcome = Math.Abs(n_money_outcome);
                if (accounts_money[account_index] - n_money_outcome >= 0)
                {
                    if (n_money_outcome != 0)
                    {
                        account_movements[account_index].Add(-n_money_outcome);
                        account_dateTimes[account_index].Add(DateTime.Now);
                        accounts_money[account_index] -= n_money_outcome;
                        Console.BackgroundColor = COLOR_SUCCESS[BG_IDX];
                        Console.ForegroundColor = COLOR_SUCCESS[FONT_IDX];
                        Console.WriteLine("The operation was succesfully performed");
                    }
                    else
                    {
                        Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                        Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                        Console.WriteLine("Zero is not a valid value. No operation was performed");
                    }
                }
                else
                {
                    Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                    Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                    Console.WriteLine("You cant outcome more money than the one you have. No operation was performed");
                }
            }
            else
            {
                Console.BackgroundColor = COLOR_ERROR[BG_IDX];
                Console.ForegroundColor = COLOR_ERROR[FONT_IDX];
                Console.WriteLine("Incorrect number format. No operation was performed");
            }
            Console.BackgroundColor = COLOR_MENU[BG_IDX];
            Console.ForegroundColor = COLOR_MENU[FONT_IDX];
            break;
        case 3:
            Console.WriteLine("Showing all operations performed");
            for (int i = 0; i < account_movements[account_index].Count; i++)
            {
                Console.WriteLine("[" + account_dateTimes[account_index][i].ToString() + "] " + account_movements[account_index][i].ToString() + coin);
            }
            break;
        case 4:
            Console.WriteLine("Showing all income operations performed");
            for (int i = 0; i < account_movements[account_index].Count; i++)
            {
                if (account_movements[account_index][i] > 0)
                {
                    Console.WriteLine("[" + account_dateTimes[account_index][i].ToString() + "] " + account_movements[account_index][i].ToString() + coin);
                }
            }
            break;
        case 5:
            Console.WriteLine("Showing all outcome operations performed");
            for (int i = 0; i < account_movements[account_index].Count; i++)
            {
                if (account_movements[account_index][i] < 0)
                {
                    Console.WriteLine("[" + account_dateTimes[account_index][i].ToString() + "] " + account_movements[account_index][i].ToString() + coin);
                }
            }
            break;
        case 6:
            Console.WriteLine("Your current money is: " + accounts_money[account_index].ToString() + coin);
            break;
        case 7:
            return;
    }
    if (n_answer != 0)
    {
        Console.Write(@"Do you wish to perform another operation? Say 'si' to do so: ");
        string? repeat = Console.ReadLine();
        if (repeat == null || repeat.ToLower() != "si")
        {
            Console.WriteLine("Your current money is: " + accounts_money[account_index].ToString() + coin);
            return;
        }
    }
    #endregion
}
#endregion