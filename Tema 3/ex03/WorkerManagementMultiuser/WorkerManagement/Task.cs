using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkerManagement
{
    public class Task
    {
        public enum Statuses
        {
            to_do,
            doing,
            done
        }
        private static uint _idCounter = 0;
        public readonly uint Id;
        public string Description { get; set; }
        public string Technology { get; set; }
        public Statuses Status { get; set; } = Statuses.to_do;
        public uint IdWorker { get; set; } = 0;

        public Task(string description, string technology)
        {
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(technology))
                throw new ArgumentException("Description and technology must not be empty or null");
            Id = ++_idCounter;
            Technology = technology;
            Description = description;
        }

        public void SetStatus(Statuses status) => Status = status;
        public void AssignITWorker(ITWorker itworker)
        {
            if (itworker == null)
                throw new ArgumentNullException("Worker must not be null");
            else if (!itworker.TechKnowledges.Contains(Technology))
                throw new ArgumentException("Worker does not have the required knowledge for this task");
            else if (Status == Statuses.done)
                throw new InvalidOperationException("Task is already done");
            IdWorker = itworker.Id;
        }
    }
}
