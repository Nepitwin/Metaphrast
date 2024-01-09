using Metaphrast.Sdk.Error;

try
{
    var metaphrast = new Metaphrast.Sdk.Metaphrast("config.json");
    Console.WriteLine("Authentication successfully");
    var usage = metaphrast.GetUsage();
    Console.WriteLine("Consumed characters : " + usage.Item1);
    Console.WriteLine("Character limit     : " + usage.Item2);
    Console.WriteLine("Start Translate Files");
    metaphrast.Translate();
    Console.WriteLine("Translate Files Complete");
    Console.WriteLine("Store Files");
    metaphrast.Save();
    Console.WriteLine("Done");
}
catch (MetaphrastException ex)
{
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}