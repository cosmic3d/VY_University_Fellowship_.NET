using System.IO;
using Bank;

SpanishBank spanishBank;
try
{
    spanishBank = new SpanishBank("ChusBanks", "2222", "1111", 20000, 5, 3000, 5);
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
    return;
}

string parentPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
string filePath = Path.Combine(parentPath, "ClientInfo.txt");
Console.WriteLine($"Writing client information to {filePath}...");
using (StreamWriter writer = new StreamWriter(filePath, false)) // 'false' indica que se sobrescribirá el archivo
{
    for (int i = 0; i < 10; i++)
    {
        spanishBank.AddClient();
        var client = spanishBank.GetClientById(i);
        if (client != null)
        {
            writer.WriteLine(client.GetClientInfo());
        }
        else
        {
            writer.WriteLine($"Client with ID {i} not found.");
        }
    }
}


SpanishBankMenu bankMenu = new SpanishBankMenu(spanishBank);

bankMenu.AccessMenu();