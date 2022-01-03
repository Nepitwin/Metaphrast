using Metaphrast.DeepL;

namespace Metaphrast.Console.Config;
internal class Config
{
    public string api_key { get; set; }
    public string source_translation_file { get; set; }

    public bool is_free_account { get; set; } = true;

    public IList<Language> translation_languages = new List<Language>();
}
