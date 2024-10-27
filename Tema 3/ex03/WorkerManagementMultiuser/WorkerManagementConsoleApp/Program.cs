using Menu;
using WorkerManagement;

WorkerManagementMenu menu = new();
List<ITWorker> workers = new();
for (uint i = 0; i < 10; i++)
{
    workers.Add(new ITWorker("Name" + i,"Surname" + i, new DateTime(2004, 1, 31), i, ITWorker.Levels.junior));
}
menu.ITWorkers = workers;
//teams
List<Team> teams = new();
for (uint i = 0; i < 10; i++)
{
    teams.Add(new Team("Team" + i));
}
menu.Teams = teams;
//add workers to teams
for (uint i = 0; i < 10; i++)
{
    try { teams[(int)i].AddManager(workers[(int)i]); }
    catch (ArgumentException) { }
    try { teams[(int)i].AddTechnician(workers[(int)i]); }
    catch (ArgumentException) { }
}

do
{
    uint Id = menu.GetUint("Enter the ID of the ITWorker account you want to log in");
    if (Id == 0)
    {
        menu.isAdmin = true;
        menu.LoginAccount = new ITWorker("admin", "admin", new DateTime(2000, 1, 1), 5, ITWorker.Levels.senior);
        Console.WriteLine("You are now logged in as admin");
        break;
    }
    if (menu.ITWorkers.Exists(x => x.Id == Id))
    {
        menu.LoginAccount = menu.ITWorkers.Find(x => x.Id == Id);
        break;
    }
    else
    {
        Console.WriteLine("Worker with that ID does not exist.");
    }
} while (true);
//look if the worker is manager
menu.MenuMask.Clear();
if (menu.Teams.Exists(x => x.Managers.Exists(y => y.Id == 0)))
{
    menu.isManager = true;
    menu.MenuMask.Add(5);
    menu.MenuMask.Add(6);
    menu.MenuMask.Add(7);
    menu.MenuMask.Add(9);
    menu.MenuMask.Add(10);
    menu.MenuMask.Add(12);
}
else
{
    menu.isManager = false;
    menu.MenuMask.Add(6);
    menu.MenuMask.Add(7);
    menu.MenuMask.Add(10);
    menu.MenuMask.Add(12);
}

menu.RunMenuWorkerManagement();