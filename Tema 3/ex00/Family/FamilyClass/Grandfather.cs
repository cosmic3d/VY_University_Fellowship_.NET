namespace FamilyClass
{
    public abstract class Grandfather
    {
        public int GrandpaId { get; set; }
        protected string GrandpaName { get; set; }
        private int GrandpaMoney { get; set; }

        protected void SetGrandpaMoney(int money)
        {
            GrandpaMoney = money;
        }

        protected int GetGrandpaMoney()
        {
            return GrandpaMoney;
        }

    }
}
