namespace WorkerManagement
{
    public abstract class Worker
    {
        protected static uint _idCounter = 0;
        internal protected uint Id;
        protected string Name { get; set; }
        protected string Surname { get; set; }
        protected DateTime BirthDate { get; set; }
        protected DateTime? LeavingDate { get; set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

    }
}
