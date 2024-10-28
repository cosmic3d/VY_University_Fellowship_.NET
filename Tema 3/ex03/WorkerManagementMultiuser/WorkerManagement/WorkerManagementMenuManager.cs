using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenuManager : WorkerManagementMenu
    {
        public WorkerManagementMenuManager() : base()
        {
            AddMethod(5, "List team members by team name", ListTeamMembersByTeamName);
            AddMethod(6, "List unassigned tasks", ListUnassignedTasks);
            AddMethod(7, "List task assignments by team name", ListTaskAssignmentsByTeamName);
            AddMethod(9, "Assign IT worker to a team as technician", AssignITWorkerAsTechnician);
            AddMethod(10, "Assign task to IT worker", AssignTaskToITWorker);
            AddMethod(12, "Exit", ExitProgram);
            BackgroundColor = ConsoleColor.Yellow;
            FontColor = ConsoleColor.Black;
            welcome = "Welcome to the Manager Menu!";
        }

        public new void ListTaskAssignmentsByTeamName()
        {
            foreach (var team in Company.Teams)
            {
                if (team.Managers.Contains(SessionITWorker) || team.Technicians.Contains(SessionITWorker))
                {
                    Console.WriteLine($"Team: {team.Name}");
                    foreach (var task in Company.Tasks)
                    {
                        ITWorker? worker = team.Managers.Find(manager => manager.Id == task.IdWorker) ?? team.Technicians.Find(technician => technician.Id == task.IdWorker);
                        if (worker != null)
                            Console.WriteLine($"{task.Id}. Task: {task.Description} - Assigned to: {worker.Name} {worker.Surname}");
                    }
                }
            }
        }

        public new void ListTeamMembersByTeamName()
        {
            foreach (var team in Company.Teams)
            {
                if (team.Managers.Contains(SessionITWorker))
                {
                    Console.WriteLine($"Team: {team.Name}");
                    Console.WriteLine("Managers:");
                    foreach (var manager in team.Managers)
                    {
                        Console.WriteLine($"{manager.Name} {manager.Surname}");
                    }
                    Console.WriteLine("Technicians:");
                    foreach (var technician in team.Technicians)
                    {
                        Console.WriteLine($"{technician.Name} {technician.Surname}");
                    }
                }
            }
        }
    }
}
