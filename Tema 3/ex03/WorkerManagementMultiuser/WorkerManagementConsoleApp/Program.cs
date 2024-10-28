using Menu;
using System.Runtime.InteropServices;
using WorkerManagement;
using WorkerManagementConsoleApp;

Company company = new Company();
ExampleData exampleData = new ExampleData(40, 10, 80);
exampleData.AddExampleData();
company.ITWorkers = exampleData.ITWorkers;
company.Teams = exampleData.Teams;
company.Tasks = exampleData.Tasks;
WorkerManagementMenu? workerManagementMenu;

do
{
    Console.Clear();
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        Console.Beep(1500, 500);
    }
    else
    {
        Console.WriteLine("\a");
    }
    Console.WriteLine("Welcome to the Worker Management Program!");
    workerManagementMenu = null;
    int Id = InputParsing.GetInt("Enter the ID of the ITWorker account you want access or '-1' to exit the program");
    if (Id == -1)
    {
        return;
    }
    else if (Id == 0)
    {
        workerManagementMenu = new WorkerManagementMenuAdmin();
        workerManagementMenu.Company = company;
    }
    else if (company.ITWorkers.Find(x => x.Id == Id) is ITWorker itworker)
    {
        foreach (var team in company.Teams)
        {
            if (team.Managers.Exists(x => x.Id == Id))
            {
                workerManagementMenu = new WorkerManagementMenuManager();
                workerManagementMenu.Company = company;
                workerManagementMenu.SessionITWorker = itworker;
                break;
            }
        }
        if (workerManagementMenu == null)
        {
            foreach (var team in company.Teams)
            {
                if (team.Technicians.Exists(x => x.Id == Id))
                {
                    workerManagementMenu = new WorkerManagementMenuITWorker();
                    workerManagementMenu.Company = company;
                    workerManagementMenu.SessionITWorker = itworker;
                    break;
                }
            }
        }
    }
    else
    {
        Console.WriteLine("Worker with that ID does not exist.");
        continue;
    }
    workerManagementMenu?.RunMenuWorkerManagement();
} while (true);