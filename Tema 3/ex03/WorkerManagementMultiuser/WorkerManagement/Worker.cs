namespace WorkerManagement
{
    public abstract class Worker
    {
        protected static uint _idCounter = 0;
        public uint Id { get; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime? LeavingDate { get; private set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        public void UnregisterWorker(DateTime leavingDate)
        {
            LeavingDate = leavingDate;
        }
    }
}
