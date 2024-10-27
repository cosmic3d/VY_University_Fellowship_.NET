using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    internal class Team
    {
        public string Name { get; set; }
        private List<ITWorker> Managers { get; } = new();
        private List<ITWorker> Technicians { get; } = new();

        public Team(string name)
        {
            Name = name;
        }

        public void AddManager(ITWorker manager)
        {
            if (manager.Level != ITWorker.Levels.Senior)
                throw new ArgumentException("Managers must be senior level");
            else if (Managers.Contains(manager))
                throw new ArgumentException("Manager already exists in the team");
            else if (Technicians.Contains(manager))
                throw new ArgumentException("Manager is already a technician in the team");
            Managers.Add(manager);
        }
    }
}
