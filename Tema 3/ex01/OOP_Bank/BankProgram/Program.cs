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

for (int i = 0; i < 10; i++)
{
    spanishBank.AddClient();
}

#if DEBUG
string? parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? Directory.GetCurrentDirectory();
string filePath = Path.Combine(parentPath, "ClientInfo.txt");
using (StreamWriter writer = new StreamWriter(filePath, false)) // 'false' indica que se sobrescribirá el archivo en cada ejecución
{
    for (int i = 0; i < 10; i++)
    {
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
//open file
System.Diagnostics.Process.Start("notepad.exe", filePath);
#endif


SpanishBankMenu bankMenu = new SpanishBankMenu(spanishBank);

bankMenu.AccessMenu();