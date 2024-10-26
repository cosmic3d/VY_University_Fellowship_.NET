using Bank;

SpanishBank spanishBank;
try
{
    spanishBank = new SpanishBank("ChusBanks", "2222", "1111",20000, 5, 3000, 5);
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
    return;
}

//create 10 clients and print their info
for (int i = 0; i < 10; i++)
{
    spanishBank.AddClient();
}
for (int i = 0; i < 10; i++)
{
    SpanishClient? client = spanishBank.GetClientById(i);
    if (client != null)
    {
        client.ShowClientInfo();
    }
}

SpanishBankMenu bankMenu = new SpanishBankMenu(spanishBank);

bankMenu.RunMenu();