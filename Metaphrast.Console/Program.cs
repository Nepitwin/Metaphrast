try
{
    var metaphrast = new Metaphrast.Metaphrast("config.json");
    Console.WriteLine("Start Translate Files");
    metaphrast.Translate();
    Console.WriteLine("Translate Files Complete");
    Console.WriteLine("Store Files");
    metaphrast.Save();
    Console.WriteLine("Done");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}