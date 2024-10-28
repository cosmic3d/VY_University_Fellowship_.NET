using System;
using System.Collections.Generic;
using WorkerManagement;

namespace WorkerManagementConsoleApp
{
    internal class ExampleData
    {
        internal List<ITWorker> ITWorkers = new();
        internal List<Team> Teams = new();
        internal List<WorkerManagement.Task> Tasks = new();

        private readonly int _numITWorkers;
        private readonly int _numTeams;
        private readonly int _numTasks;

        public ExampleData(int numITWorkers = 35, int numTeams = 10, int numTasks = 40)
        {
            _numITWorkers = numITWorkers;
            _numTeams = numTeams;
            _numTasks = numTasks;
        }

        public void AddExampleData()
        {
            DateTime currentDate = DateTime.Now;

            // Crear ITWorkers con niveles variados según el número especificado
            for (uint i = 0; i < _numITWorkers; i++)
            {
                try
                {
                    var level = i < _numITWorkers * 0.7 ? ITWorker.Levels.junior : ITWorker.Levels.senior;

                    // Calcular años de experiencia como un valor menor a la edad en años
                    DateTime birthDate = new DateTime(2004, 1, 31);
                    int age = currentDate.Year - birthDate.Year;
                    int yearsOfExperience = Math.Max(0, age - 2); // Usar un valor seguro, por ejemplo, 2 años menos que la edad

                    if (yearsOfExperience >= age)
                    {
                        throw new ArgumentException("Years of experience cannot exceed or equal age.");
                    }

                    ITWorkers.Add(new ITWorker("Name" + i, "Surname" + i, birthDate, (uint)yearsOfExperience, level));
                }
                catch (ArgumentException e)
                {
                    #if DEBUG
                        Console.WriteLine($"Error adding ITWorker {i}: {e.Message}");
                    #endif
                }
            }

            // Crear equipos según el número especificado
            for (uint i = 0; i < _numTeams; i++)
            {
                try
                {
                    Teams.Add(new Team("Team" + i));
                }
                catch (ArgumentException e)
                {
                    #if DEBUG
                        Console.WriteLine($"Error adding Team {i}: {e.Message}");
                    #endif
                }
            }

            // Asignar managers a cada equipo (usando los últimos ITWorkers disponibles)
            for (uint i = 1; i <= _numTeams; i++)
            {
                try
                {
                    Teams[(int)(i - 1)].AddManager(ITWorkers[^(int)i]);
                }
                catch (ArgumentException e)
                {
                    #if DEBUG
                        Console.WriteLine($"Error assigning manager to Team {(int)(i - 1)}: {e.Message}");
                    #endif
                }
            }

            // Asignar técnicos a los equipos distribuyéndolos de forma cíclica
            for (uint i = 1; i <= _numITWorkers; i++)
            {
                try
                {
                    Teams[(int)(i - 1) % _numTeams].AddTechnician(ITWorkers[^(int)i]);
                }
                catch (ArgumentException e)
                {
                    #if DEBUG
                        Console.WriteLine($"Error assigning technician to Team {(int)(i - 1) % _numTeams}: {e.Message}");
                    #endif
                }
            }

            // Crear tareas según el número especificado
            for (uint i = 0; i < _numTasks; i++)
            {
                try
                {
                    Tasks.Add(new WorkerManagement.Task("Description" + i, "Technology" + i));
                }
                catch (ArgumentException e)
                {
                    #if DEBUG
                        Console.WriteLine($"Error adding Task {i}: {e.Message}");
                    #endif
                }
            }
        }
    }
}
