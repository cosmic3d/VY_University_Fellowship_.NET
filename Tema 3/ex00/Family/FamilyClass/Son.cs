namespace FamilyClass
{
    public class Son : Father
    {
        public int SonId { get; set; }
        protected string SonName { get; set; }
        private int SonMoney { get; set; }

        public Son(
                    int id, string name, int money,
                    int fatherId, string fatherName, int fatherMoney,
                    int grandpaId, string grandpaName, int grandpaMoney
                  )
        {
            GrandpaId = grandpaId;
            GrandpaName = grandpaName;
            SetGrandpaMoney(grandpaMoney);
            FatherId = fatherId;
            FatherName = fatherName;
            SetFatherMoney(fatherMoney);
            SonId = id;
            SonName = name;
            SonMoney = money;
        }

        public void ShowFamilyFields()
        {
            Console.WriteLine($"GrandpaId: {GrandpaId}, GrandpaName: {GrandpaName}, GrandpaMoney: {GetGrandpaMoney()}");
            Console.WriteLine($"FatherId: {FatherId}, FatherName: {FatherName}, FatherMoney: {GetFatherMoney()}");
            Console.WriteLine($"SonId: {SonId}, SonName: {SonName}, SonMoney: {SonMoney}");
        }

        public void SetFamilyFields(int grandpaId, string grandpaName, int grandpaMoney, int fatherId, string fatherName, int fatherMoney, int sonId, string sonName, int sonMoney)
        {
            GrandpaId = grandpaId;
            GrandpaName = grandpaName;
            SetGrandpaMoney(grandpaMoney);
            FatherId = fatherId;
            FatherName = fatherName;
            SetFatherMoney(fatherMoney);
            SonId = sonId;
            SonName = sonName;
            SonMoney = sonMoney;
            Console.WriteLine("Family fields have been set.");
            ShowFamilyFields();
        }
    }
}
