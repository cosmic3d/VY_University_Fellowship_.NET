using Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenuITWorker : WorkerManagementMenu
    {
        public WorkerManagementMenuITWorker() : base()
        {
            AddMethod(5, "List team members by team name", ListTeamMembersByTeamName);
            AddMethod(6, "List unassigned tasks", ListUnassignedTasks);
            AddMethod(7, "List task assignments by team name", ListTaskAssignmentsByTeamName);
            AddMethod(9, "Assign IT worker to a team as technician", AssignITWorkerAsTechnician);
            AddMethod(10, "Assign task to IT worker", AssignTaskToITWorker);
            AddMethod(12, "Exit", ExitProgram);
            BackgroundColor = ConsoleColor.DarkBlue;
            FontColor = ConsoleColor.White;
            welcome = "Welcome to the IT Worker Menu!";
        }

        public new void ListTaskAssignmentsByTeamName()
        {
            foreach (var team in Company.Teams)
            {
                if (team.Technicians.Contains(SessionITWorker) || team.Managers.Contains(SessionITWorker))
                {
                    Console.WriteLine($"Team: {team.Name}");
                    foreach (var task in Company.Tasks)
                    {
                        ITWorker? worker = team.Technicians.Find(manager => manager.Id == task.IdWorker) ?? team.Managers.Find(technician => technician.Id == task.IdWorker);
                        if (worker != null)
                            Console.WriteLine($"{task.Id}. Task: {task.Description} - Assigned to: {worker.Name} {worker.Surname}");
                    }
                }
            }
        }

        public new void AssignTaskToITWorker() {
            uint taskId = InputParsing.GetUint("Enter the ID of the task you want to assign");
            Task task = Company.Tasks.Find(task => task.Id == taskId);
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }
            if (task.IdWorker != 0)
            {
                Console.WriteLine("Task already assigned");
                return;
            }
            try
            {
                task.AssignITWorker(SessionITWorker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not assign task to IT worker: {ex.Message}");
                return;
            }
            Console.WriteLine("Task assigned successfully");
        }
    }
}
