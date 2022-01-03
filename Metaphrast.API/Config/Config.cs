using Metaphrast.DeepL;
using Newtonsoft.Json;

namespace Metaphrast.Console.Config;
internal class Config
{
    [JsonProperty(PropertyName = "api_key")]
    public string ApiKey { get; set; } = "";
    [JsonProperty(PropertyName = "source_translation_file")]
    public string SourceTranslationFile { get; set; } = "";
    [JsonProperty(PropertyName = "is_free_account")]
    public bool IsFreeAccount { get; set; } = true;
    [JsonProperty(PropertyName = "translation_languages")]
    public IList<Language> TranslationLanguages = new List<Language>();
}
