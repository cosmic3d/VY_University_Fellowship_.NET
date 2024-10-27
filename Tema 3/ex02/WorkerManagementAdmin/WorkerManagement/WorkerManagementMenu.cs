using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenu : Menu.Menu
    {
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
        private void RegisterNewITWorker()
        {
            Console.WriteLine("Registering new IT Worker");
        }

        private void AddTeam()
        {
            Console.WriteLine("Adding Team");
        }

        private void AddTask()
        {
            Console.WriteLine("Adding Task");
        }

        private void ListAllTeamNames()
        {
            Console.WriteLine("Listing all team names");
        }

        private void ListTeamMembersByTeamName()
        {
            Console.WriteLine("Listing team members by team name");
        }

        private void ListUnassignedTasks()
        {
            Console.WriteLine("Listing unassigned tasks");
        }

        private void ListTaskAssignmentsByTeamName()
        {
            Console.WriteLine("Listing task assignments by team name");
        }

        private void AssignITWorkerAsManager()
        {
            Console.WriteLine("Assigning IT worker to a team as manager");
        }

        private void AssignITWorkerAsTechnician()
        {
            Console.WriteLine("Assigning IT worker to a team as technician");
        }

        private void AssignTaskToITWorker()
        {
            Console.WriteLine("Assigning task to IT worker");
        }

        private void UnregisterITWorker()
        {
            Console.WriteLine("Unregistering IT worker");
        }

        protected override void ExitProgram()
        {
            Console.WriteLine("Exiting WorkerManagement program...");
        }
    }
}
