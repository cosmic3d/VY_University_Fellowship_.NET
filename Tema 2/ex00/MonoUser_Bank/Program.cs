const int ARR_SIZE = 100000;
const int MAX_INCOME = 5000;

int client_money = 0;
int[] movements = new int[ARR_SIZE];
DateTime[] dateTimes = new DateTime[ARR_SIZE];
int movement_idx = 0; // Assignation index to keep track of where to assign the next value
int movements_size = 0; // Iteration index to keep track of how many values were assigned
int n_answer = 0;
const char coin = '€';

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (n_answer != 7)
{
    #region THEME
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Clear();
    #endregion

    #region BANNER
    string banner = @"
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



    Console.ForegroundColor = ConsoleColor.White;
    switch (n_answer)
    {
        case 0:
            break;
        case 1:
            Console.Write("Insert the amount of money income: ");
            string? money_income = Console.ReadLine();
            int n_money_income = 0;
            bool success_income = int.TryParse(money_income, out n_money_income);
            if (success_income && n_money_income >= 0 && Math.Abs(n_money_income) <= MAX_INCOME)
            {
                n_money_income = Math.Abs(n_money_income);
                if (n_money_income != 0)
                {
                    movements[movement_idx % ARR_SIZE] = n_money_income;
                    dateTimes[movement_idx % ARR_SIZE] = DateTime.Now;
                    if (client_money + n_money_income > 0)
                    {
                        client_money += n_money_income;
                    }
                    else
                    {
                        Console.WriteLine("You have reached the maximum amount of money");
                        n_money_income = int.MaxValue - client_money;
                    }
                    movement_idx++;
                    movements_size++;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The operation was succesfully performed");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("DO U REALLY WISH TO DO NOTHING? No operation was performed");
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
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
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            break;
        case 2:
            Console.Write("Insert the amount of money outcome: ");
            string? money_outcome = Console.ReadLine();
            int n_money_outcome = 0;
            bool success_outcome = int.TryParse(money_outcome, out n_money_outcome);
            if (success_outcome)
            {
                n_money_outcome = Math.Abs(n_money_outcome);
                if (client_money - n_money_outcome >= 0)
                {
                    if (n_money_outcome != 0)
                    {
                        movements[movement_idx % ARR_SIZE] = -n_money_outcome;
                        dateTimes[movement_idx % ARR_SIZE] = DateTime.Now;
                        client_money -= n_money_outcome;
                        movement_idx++;
                        movements_size++;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The operation was succesfully performed");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("DO U REALLY WISH TO DO NOTHING? No operation was performed");
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You cant outcome more money than the one you have. No operation was performed");
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect number format. No operation was performed");
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            break;
        case 3:
            Console.WriteLine("Showing all operations performed");
            for (int i = 0; i < movements_size && i < ARR_SIZE; i++)
            {
                Console.WriteLine("[" + dateTimes[i].ToString() + "] " + movements[i].ToString() + coin);
            }
            break;
        case 4:
            Console.WriteLine("Showing all income operations performed");
            for (int i = 0; i < movements_size && i < ARR_SIZE; i++)
            {
                if (movements[i] > 0)
                {
                    Console.WriteLine("[" + dateTimes[i].ToString() + "] " + movements[i].ToString() + coin);
                }
            }
            break;
        case 5:
            Console.WriteLine("Showing all outcome operations performed");
            for (int i = 0; i < movements_size && i < ARR_SIZE; i++)
            {
                if (movements[i] < 0)
                {
                    Console.WriteLine("[" + dateTimes[i].ToString() + "] " + movements[i].ToString() + coin);
                }
            }
            break;
        case 6:
            Console.WriteLine("Your current money is: " + client_money.ToString() + coin);
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
            Console.WriteLine("Your current money is: " + client_money.ToString() + coin);
            return;
        }
    }
    #endregion
}