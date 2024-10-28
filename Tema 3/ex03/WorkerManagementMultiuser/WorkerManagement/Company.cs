using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class Company
    {
        public List<ITWorker> ITWorkers { get; set; } = new();
        public List<Team> Teams { get; set; } = new();
        public List<Task> Tasks { get; set; } = new();

        public Company()
        {
        }
        public Company(List<ITWorker> iTWorkers, List<Team> teams, List<Task> tasks)
        {
            ITWorkers = iTWorkers;
            Teams = teams;
            Tasks = tasks;
        }
    }
}
