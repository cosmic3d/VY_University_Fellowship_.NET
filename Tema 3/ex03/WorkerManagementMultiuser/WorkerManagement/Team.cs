using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkerManagement
{
    public class Team
    {
        public string Name { get; set; }
        public List<ITWorker> Managers { get; private set; } = new();
        public List<ITWorker> Technicians { get; private set; } = new();

        public Team(string name)
        {
            Name = name;
        }

        public void AddManager(ITWorker manager)
        {
            if (manager.Level != ITWorker.Levels.senior)
                throw new ArgumentException("Managers must be senior level");
            else if (Managers.Contains(manager))
                throw new ArgumentException("Manager already exists in the team");
            else if (Technicians.Contains(manager))
                throw new ArgumentException("Manager is already a technician in the team");
            Managers.Add(manager);
        }

        public void AddTechnician(ITWorker technician)
        {
            if (Technicians.Contains(technician))
                throw new ArgumentException("Technician already exists in the team");
            else if (Managers.Contains(technician))
                throw new ArgumentException("Technician is already a manager in the team");
            Technicians.Add(technician);
        }
    }
}
