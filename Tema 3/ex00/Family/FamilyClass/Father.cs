namespace FamilyClass
{
    public abstract class Father : Grandfather
    {
        public int FatherId { get; set; }
        protected string FatherName { get; set; }
        private int FatherMoney { get; set; }

        protected void SetFatherMoney(int money)
        {
            FatherMoney = money;
        }

        protected int GetFatherMoney()
        {
            return FatherMoney;
        }
    }
}
