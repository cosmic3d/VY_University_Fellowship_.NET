using Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public interface IWorkerManagement
    {
        // Propiedades para almacenar listas de ITWorkers, Teams y Tasks
        public Company Company { get; set; }

        // Método principal para ejecutar el menú de gestión de trabajadores
        void RunMenuWorkerManagement();

        // Métodos relacionados con la gestión de trabajadores, equipos y tareas
        void RegisterNewITWorker();
        void AddTeam();
        void AddTask();
        void ListAllTeamNames();
        void ListTeamMembersByTeamName();
        void ListUnassignedTasks();
        void ListTaskAssignmentsByTeamName();

        // Métodos para asignaciones y cambios de estado
        void AssignITWorkerAsManager();
        void AssignITWorkerAsTechnician();
        void AssignTaskToITWorker();
        void ChangeTaskStatus();

        // Métodos para la administración y remoción de trabajadores
        void UnregisterITWorker();

        // Método de salida del programa
        void ExitProgram();

        // Métodos auxiliares para obtener datos de nivel y estado (protected en clase, público en interfaz)
        ITWorker.Levels GetLevel(string requirement);
        Task.Statuses GetStatus(string requirement);
    }
    public class WorkerManagementMenu : Menu.Menu, IWorkerManagement
    {
        public ITWorker? SessionITWorker { get; set; }
        public Company Company { get; set; } = new Company();
        public WorkerManagementMenu()
        {
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

        public virtual void RunMenuWorkerManagement()
        {
            RunMenu();
        }
        public virtual void RegisterNewITWorker()
        {
            try
            {
                Company.ITWorkers.Add(new ITWorker(
                    InputParsing.GetString("Enter name of the worker"),
                    InputParsing.GetString("Enter surname of the worker"),
                    InputParsing.GetDateTime("Enter birthdate of the worker"),
                    InputParsing.GetUint("Enter the years of experience of the worker"),
                    GetLevel("Enter the level of the worker")
                    ));
                string techKnowledge;
                while (true)
                {
                    techKnowledge = InputParsing.GetString("Enter the tech knowledge of the worker (or type 'exit' to finish)");
                    if (techKnowledge.ToLower() == "exit")
                        break;
                    Company.ITWorkers[^1].AddTechKnowledge(techKnowledge);
                }
                Console.WriteLine($"Worker with id {Company.ITWorkers[^1].Id} registered successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new worker: {ex.Message}");
            }
        }


        public virtual void AddTeam()
        {
            try
            {
                AddTeamToList(new Team(InputParsing.GetString("Enter the name of the team")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new team: {ex.Message}");
            }
        }

        public virtual void AddTeamToList(Team team)
        {
            foreach (var t in Company.Teams)
            {
                if (t.Name == team.Name)
                {
                    throw new ArgumentException("Team already exists");
                }
            }
            Company.Teams.Add(team);
        }

        public virtual void AddTask()
        {
            try
            {
                Company.Tasks.Add(new Task(InputParsing.GetString("Enter the description of the task"), InputParsing.GetString("Enter technology of the task")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not register a new task: {ex.Message}");
            }
        }

        public virtual void ListAllTeamNames()
        {
            //print every team name along with a number in order with a foreach
            Console.WriteLine("Listing all team names");
            foreach (var team in Company.Teams)
            {
                Console.WriteLine(team.Name);
            }
        }

        public virtual void ListTeamMembersByTeamName()
        {
            string teamName = InputParsing.GetString("Enter the name of the team");
            Team? team = Company.Teams.Find(t => t.Name == teamName);
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

        public virtual void ListUnassignedTasks()
        {
            foreach (var task in Company.Tasks)
            {
                if (task.IdWorker == 0)
                {
                    Console.WriteLine($"Task: {task.Description}");
                }
            }
        }

        public virtual void ListTaskAssignmentsByTeamName()
        {
            string teamName = InputParsing.GetString("Enter the name of the team");
            Team? team = Company.Teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            Console.WriteLine($"Listing task assignments of {teamName}");
            foreach (var task in Company.Tasks)
            {
                if (task.IdWorker != 0 && team.Technicians.Find(itworker => itworker.Id == task.IdWorker) != null)
                {
                    Console.WriteLine($"Task: {task.Description} assigned to {task.IdWorker}");
                }
            }
        }

        public virtual void AssignITWorkerAsManager()
        {
            string teamName = InputParsing.GetString("Enter the name of the team");
            Team? team = Company.Teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            uint workerId = InputParsing.GetUint("Enter the ID of the IT worker");
            ITWorker? itworker = Company.ITWorkers.Find(itw => itw.Id == workerId);
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

        public virtual void AssignITWorkerAsTechnician()
        {
            string teamName = InputParsing.GetString("Enter the name of the team");
            Team? team = Company.Teams.Find(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine("Team not found");
                return;
            }
            uint workerId = InputParsing.GetUint("Enter the ID of the IT worker");
            ITWorker? itworker = Company.ITWorkers.Find(itw => itw.Id == workerId);
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

        public virtual void AssignTaskToITWorker()
        {
            uint workerId = InputParsing.GetUint("Enter the ID of the IT worker");
            ITWorker? itworker = Company.ITWorkers.Find(itw => itw.Id == workerId);
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            uint taskId = InputParsing.GetUint("Enter the ID of the task");
            Task? task = Company.Tasks.Find(t => t.Id == taskId);
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

        public virtual void ChangeTaskStatus()
        {
            uint taskId = InputParsing.GetUint("Enter the ID of the task");
            Task? task = Company.Tasks.Find(t => t.Id == taskId);
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }
            task.SetStatus(GetStatus("Enter the new status of the task"));
        }

        public virtual void UnregisterITWorker()
        {
            uint workerId = InputParsing.GetUint("Enter the ID of the IT worker");
            ITWorker? itworker = Company.ITWorkers.Find(itw => itw.Id == workerId);
            if (itworker == null)
            {
                Console.WriteLine("IT worker not found");
                return;
            }
            itworker.UnregisterWorker(DateTime.Now);
            foreach (Team team in Company.Teams)
            {
                if (team.Managers.Contains(itworker))
                    team.Managers.Remove(itworker);
                if (team.Technicians.Contains(itworker))
                    team.Technicians.Remove(itworker);
            }
            foreach (Task task in Company.Tasks)
            {
                if (task.IdWorker == itworker.Id)
                    task.IdWorker = 0;
            }
            Company.ITWorkers.Remove(itworker);
        }

        public override void ExitProgram()
        {
            Console.WriteLine("Exiting WorkerManagement program...");
        }

        public virtual ITWorker.Levels GetLevel(string requirement)
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

        public virtual Task.Statuses GetStatus(string requirement)
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
