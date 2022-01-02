using Metaphrast.DeepL;

namespace Metaphrast.Console.Config;
internal class Config
{
    public string api { get; set; }
    public string source { get; set; }

    public IList<Language> languages = new List<Language>();
}
