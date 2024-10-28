using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenu : Menu.Menu
    {
        private List<ITWorker> _itworkers = new();
        private List<Team> _teams = new();
        private List<Task> _tasks = new();
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
            AddMethod(11, "Change task status", ChangeTaskStatus);
            AddMethod(12, "Unregister IT worker", UnregisterITWorker);
            AddMethod(13, "Exit", ExitProgram);
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

        public void RunMenuWorkerManagement()
        {
            RunMenu();
        }
        public void RegisterNewITWorker()
        {
            try
            {
                _itworkers.Add(new ITWorker(
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
                    _itworkers[^1].AddTechKnowledge(techKnowledge);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new worker: {ex.Message}");
            }
        }


        public void AddTeam()
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

        public void AddTeamToList(Team team) {
            foreach (var t in _teams)
            {
                if (t.Name == team.Name)
                {
                    throw new ArgumentException("Team already exists");
                }
            }
            _teams.Add(team);
        }

        public void AddTask()
        {
            try
            {
                _tasks.Add(new Task(GetString("Enter the description of the task"), GetString("Enter technology of the task")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new task: {ex.Message}");
            }
        }

        public void ListAllTeamNames()
        {
            //print every team name along with a number in order with a foreach
            Console.WriteLine("Listing all team names");
            foreach (var team in _teams)
            {
                Console.WriteLine(team.Name);
            }
        }

        public void ListTeamMembersByTeamName()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = _teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            Console.WriteLine($"Listing team members of {teamName}");
            foreach (var manager in team.Managers)
            {
                Console.WriteLine($"Manager: {manager.Name} {manager.Surname}");
            }
            foreach (var technician in team.Technicians)
            {
                Console.WriteLine($"Technician: {technician.Name} {technician.Surname}");
            }
        }

        public void ListUnassignedTasks()
        {
            foreach (var task in _tasks)
            {
                if (task.IdWorker == 0)
                {
                    Console.WriteLine($"Task: {task.Description}");
                }
            }
        }

        public void ListTaskAssignmentsByTeamName()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = _teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            Console.WriteLine($"Listing task assignments of {teamName}");
            foreach (var task in _tasks)
            {
                if (task.IdWorker != 0 && team.Technicians.Find(itworker => itworker.Id == task.IdWorker) != null)
                {
                    Console.WriteLine($"Task: {task.Description} assigned to {task.IdWorker}");
                }
            }
        }

        public void AssignITWorkerAsManager()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = _teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            ITWorker? itworker = _itworkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
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

        public void AssignITWorkerAsTechnician()
        {
            string teamName = GetString("Enter the name of the team");
            Team? team = _teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            ITWorker? itworker = _itworkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
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

        public void AssignTaskToITWorker()
        {
            ITWorker? itworker = _itworkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            Task? task = _tasks.Find(t => t.Id == GetUint("Enter the ID of the task"));
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

        public void ChangeTaskStatus()
        {
            Task? task = _tasks.Find(t => t.Id == GetUint("Enter the ID of the task"));
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }
            task.SetStatus(GetStatus("Enter the new status of the task"));
        }

        public void UnregisterITWorker()
        {
            ITWorker? itworker = _itworkers.Find(itw => itw.Id == GetUint("Enter the ID of the IT worker"));
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            itworker.UnregisterWorker(DateTime.Now);
            foreach (Team team in _teams)
            {
                if (team.Managers.Contains(itworker))
                    team.Managers.Remove(itworker);
                if (team.Technicians.Contains(itworker))
                    team.Technicians.Remove(itworker);
            }
            foreach (Task task in _tasks)
            {
                if (task.IdWorker == itworker.Id)
                    task.IdWorker = 0;
            }
            _itworkers.Remove(itworker);
        }

        public override void ExitProgram()
        {
            Console.WriteLine("Exiting WorkerManagement program...");
        }

        public ITWorker.Levels GetLevel(string requirement)
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

        public Task.Statuses GetStatus(string requirement)
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
