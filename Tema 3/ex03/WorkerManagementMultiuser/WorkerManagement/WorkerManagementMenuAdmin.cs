using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class WorkerManagementMenuAdmin : WorkerManagementMenu
    {
        public WorkerManagementMenuAdmin() : base()
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
            BackgroundColor = ConsoleColor.Red;
            FontColor = ConsoleColor.White;
            welcome = "Welcome to the Admin Menu!";
        }
    }
}
