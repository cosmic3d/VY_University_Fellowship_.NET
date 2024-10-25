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

SpanishClient spanishClient = new SpanishClient(spanishBank);
spanishClient.ShowClientInfo();