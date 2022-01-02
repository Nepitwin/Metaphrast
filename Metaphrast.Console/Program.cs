try
{
    var metaphrast = new Metaphrast.Metaphrast("config.json");
    metaphrast.Translate();
    metaphrast.Save();
    Console.WriteLine("Done");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}