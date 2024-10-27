using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManagement
{
    public class ITWorker : Worker
    {
        public enum Levels
        {
            Junior,
            Medium,
            Senior
        }
        public uint YearsOfExperience { get; private set; }
        public List<string> TechKnowledges { get; } = new();
        public Levels Level { get; private set; } = Levels.Junior;

        public ITWorker(string name, string surname, DateTime birthDate, uint yearsOfExperience, Levels level) : base(name, surname, birthDate)
        {
            if (yearsOfExperience < 0)
                throw new ArgumentException("Years of experience must be greater than 0");
            else if (yearsOfExperience > DateTime.Now.Year - birthDate.Year)
                throw new ArgumentException("Years of experience must be less than the age of the worker");
            else if (DateTime.Now.Year - birthDate.Year < 18)
                throw new ArgumentException("User must be at least 18 years old");
            else if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Name and surname must not be empty or null");
            Id = ++_idCounter;
            YearsOfExperience = yearsOfExperience;
            SetLevel(level);
        }

        public void SetLevel(Levels level) => Level = level == Levels.Senior && YearsOfExperience >= 5 ? level : Levels.Junior;

        public void AddTechKnowledge(string techKnowledge)
        {
            if (string.IsNullOrWhiteSpace(techKnowledge))
                throw new ArgumentException("Tech knowledge must not be empty or null");
            else if (TechKnowledges.Contains(techKnowledge))
                throw new ArgumentException("Tech knowledge already added");
            TechKnowledges.Add(techKnowledge);
        }

        public void RemoveTechKnowledge(string techKnowledge)
        {
            TechKnowledges.Remove(techKnowledge);
        }
    }
}
