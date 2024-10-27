using Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenu : Menu.Menu
    {
        public bool isManager = false;
        public bool isAdmin = false;
        public ITWorker LoginAccount;
        public List<int> MenuMask = new();
        public List<ITWorker> ITWorkers = new();
        public List<Team> Teams = new();
        public List<Task> Tasks = new();
        public WorkerManagementMenu()
        {
            AddMethod(1, "Register new IT worker", RegisterNewITWorker);
            AddMethod(2, "Register new team", AddTeam);
            AddMethod(3, "Register new task (unassigned to anyone)", AddTask);
            AddMethod(4, "List all team names", ListAllTeamNames);
            AddMethod(5, "List team members by team name", ListTeamMembersByTeamName);
            AddMethod(6, "List unassigned tasks", ListUnassignedTasks);
            AddMethod(7, "List task assignments by team name", ListTaskAssignmentsByTeamName);
            AddMethod(8, "Assign IT worker to a team as manager", AssignITWorkerAsManager);
            AddMethod(9, "Assign IT worker to a team as technician", AssignITWorkerAsTechnician);
            AddMethod(10, "Assign task to IT worker", AssignTaskToITWorker);
            AddMethod(11, "Unregister IT worker", UnregisterITWorker);
            AddMethod(12, "Exit", ExitProgram);
            AddMethod(13, "Change task status", ChangeTaskStatus); //Cambiamos de sitio para que no haya conflicto con el testing según el enunciado
            banner = @"
    ___  ___          ___                                          
    | |  | |          | |                                         
    | |  | | ___  _ __| | _____ _ __                              
    | |/\| |/ _ \| '__| |/ / _ \ '__|                             
    \  /\  / (_) | |  |   <  __/ |                                
     \/  \/ \___/|_|  |_|\_\___|_|                                
                                                                  
                                                                  
    ___  ___                                                  _   
    |  \/  |                                                 | |  
    | .  . | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_ 
    | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|
    | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_ 
    \_|  |_/\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__|
                               __/ |                              
                              |___/                               ";
            BackgroundColor = ConsoleColor.Cyan;
            FontColor = ConsoleColor.Black;

        }

        public override void AddMethod(int id, string description, Action method)
        {
            if (MenuMask.Contains(id))
            {
                Methods.Add(new MenuMethod(id, description, method));
            }
        }

        public void RunMenuWorkerManagement()
        {
            RunMenu();
        }
        private void RegisterNewITWorker()
        {
            try
            {
                ITWorkers.Add(new ITWorker(
                    GetString("Enter name of the worker"),
                    GetString("Enter surname of the worker"),
                    GetDateTime("Enter birthdate of the worker"),
                    GetUint("Enter the years of experience of the worker"),
                    GetLevel("Enter the level of the worker")
                    ));
                string techKnowledge;
                while (true)
                {
                    techKnowledge = GetString("Enter the tech knowledge of the worker (or type 'exit' to finish)");
                    if (techKnowledge.ToLower() == "exit")
                        break;
                    ITWorkers[^1].AddTechKnowledge(techKnowledge);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new worker: {ex.Message}");
            }
        }


        private void AddTeam()
        {
            try
            {
                AddTeamToList(new Team(GetString("Enter the name of the team")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new team: {ex.Message}");
            }
        }

        private void AddTeamToList(Team team) {
            foreach (var t in Teams)
            {
                if (t.Name == team.Name)
                {
                    throw new ArgumentException("Team already exists");
                }
            }
            Teams.Add(team);
        }

        private void AddTask()
        {
            try
            {
                Tasks.Add(new Task(GetString("Enter the description of the task"), GetString("Enter technology of the task")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new task: {ex.Message}");
            }
        }

        private void ListAllTeamNames()
        {
            //print every team name along with a number in order with a foreach
            Console.WriteLine("Listing all team names");
            foreach (var team in Teams)
            {
                Console.WriteLine(team.Name);
            }
        }

        private void ListTeamMembersByTeamName()
        {
            //if is a manager only list the members of their teams
            if (isManager)
            {
                Console.WriteLine($"Listing team members of {LoginAccount.Name}");
                foreach (var team in Teams)
                {
                    if (team.Managers.Contains(LoginAccount))
                    {
                        Console.WriteLine($"Team: {team.Name}");
                        Console.WriteLine("Managers:");
                        foreach (var manager in team.Managers)
                        {
                            Console.WriteLine($"- {manager.Name} {manager.Surname}");
                        }
                        Console.WriteLine("Technicians:");
                        foreach (var technician in team.Technicians)
                        {
                            Console.WriteLine($"- {technician.Name} {technician.Surname}");
                        }
                    }
                }
            }
            else
            {
                string teamName = GetString("Enter the name of the team");
                Team? team = Teams.Find(t => t.Name == teamName);
                if (team == null)
                {
                    Console.WriteLine("Team not found");
                    return;
                }
                Console.WriteLine($"Listing team members of {teamName}");
                Console.WriteLine("Managers:");
                foreach (var manager in team.Managers)
                {
                    Console.WriteLine($"- {manager.Name} {manager.Surname}");
                }
                Console.WriteLine("Technicians:");
                foreach (var technician in team.Technicians)
                {
                    Console.WriteLine($"- {technician.Name} {technician.Surname}");
                }
            }
        }

        private void ListUnassignedTasks()
        {
            foreach (var task in Tasks)
            {
                if (task.IdWorker == 0)
                {
                    Console.WriteLine($"Task: {task.Description}");
                }
            }
        }

        private void ListTaskAssignmentsByTeamName()
        {
            //ONly list the tasks of the teams of the manager
            foreach (var task in Tasks) {
                if (isManager)
                {
                    foreach (var team in Teams)
                    {
                        if (team.Managers.Contains(LoginAccount))
                        {
                            if (task.IdWorker != 0)
                            {
                                Console.WriteLine($"Task: {task.Description}");
                                Console.WriteLine($"Assigned to: {ITWorkers.Find(x => x.Id == task.IdWorker).Name} {ITWorkers.Find(x => x.Id == task.IdWorker).Surname}");
                            }
                        }
                    }
                }
                else
                {
                    foreach (var team in Teams)
                    {
                        if (team.Managers.Contains(LoginAccount) || team.Technicians.Contains(LoginAccount))
                        {
                            if (task.IdWorker != 0)
                            {
                                Console.WriteLine($"Task: {task.Description}");
                                Console.WriteLine($"Assigned to: {ITWorkers.Find(x => x.Id == task.IdWorker).Name} {ITWorkers.Find(x => x.Id == task.IdWorker).Surname}");
                            }
                        }
                    }
                }
            }
        }

        private void AssignITWorkerAsManager()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = Teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            ITWorker? itworker = ITWorkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            try
            {
                team.AddManager(itworker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not assign IT worker as manager: {ex.Message}");
            }
        }

        private void AssignITWorkerAsTechnician()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = Teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            ITWorker? itworker = ITWorkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            try
            {
                team.AddTechnician(itworker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not assign IT worker as technician: {ex.Message}");
            }
        }

        private void AssignTaskToITWorker()
        {
            ITWorker? itworker;
            if (isManager)
            {
                itworker = ITWorkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
                if (itworker == null)
                {
                    Console.WriteLine("IT worker not found");
                    return;
                }
            }
            else
            {
                itworker = LoginAccount;
            }
            Task? task = Tasks.Find(t => t.Id == GetUint("Enter the ID of the task"));
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }
            try
            {
                task.AssignITWorker(itworker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not assign task to IT worker: {ex.Message}");
            }
        }

        private void ChangeTaskStatus()
        {
            Task? task = Tasks.Find(t => t.Id == GetUint("Enter the ID of the task"));
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }
            task.SetStatus(GetStatus("Enter the new status of the task"));
        }

        private void UnregisterITWorker()
        {
            ITWorker? itworker = ITWorkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            itworker.UnregisterWorker(DateTime.Now);
            foreach (Team team in Teams)
            {
                if (team.Managers.Contains(itworker))
                    team.Managers.Remove(itworker);
                if (team.Technicians.Contains(itworker))
                    team.Technicians.Remove(itworker);
            }
            foreach (Task task in Tasks)
            {
                if (task.IdWorker == itworker.Id)
                    task.IdWorker = 0;
            }
            ITWorkers.Remove(itworker);
        }

        protected override void ExitProgram()
        {
            Console.WriteLine("Exiting WorkerManagement program...");
        }

        protected ITWorker.Levels GetLevel(string requirement)
        {
            Console.Write(requirement + ": ");
            while (true)
            {
                string? input = Console.ReadLine()?.ToLower().Trim();
                if (input != "junior" && input != "medium" && input != "senior")
                {
                    Console.WriteLine("Invalid level. Please enter a valid level (junior, medium, senior)");
                    Console.Write(requirement + ": ");
                    continue;
                }
                ITWorker.Levels level = input == "junior" ? ITWorker.Levels.junior : input == "medium" ? ITWorker.Levels.medium : ITWorker.Levels.senior;
                return level;
            }
        }

        protected Task.Statuses GetStatus(string requirement)
        {
            Console.Write(requirement + ": ");
            while (true)
            {
                string? input = Console.ReadLine()?.ToLower().Trim();
                if (input != "to_do" && input != "doing" && input != "done")
                {
                    Console.WriteLine("Invalid status. Please enter a valid status (to_do, doing, done)");
                    Console.Write(requirement + ": ");
                    continue;
                }
                Task.Statuses status = input == "to_do" ? Task.Statuses.to_do : input == "doing" ? Task.Statuses.doing : Task.Statuses.done;
                return status;
            }
        }
    }
}
