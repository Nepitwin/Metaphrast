using Newtonsoft.Json;

namespace MetaphrastSDK.DeepL.Response;

/**
 * JSON DeepL response from translation API.
 *
 * Input ["Netherlands", "Hello World"]
 *
 * Multiple german translation response example:
 * {
 *  'translations': [
 *      {
 *          'detected_source_language': 'EN',
 *          'text': 'Niederlande'
 *      },
 *      {
 *           'detected_source_language': 'EN',
 *          'text': 'Hallo, Welt!'
 *      }
 *  ]
 * }
 */
internal class TranslationsResponse
{
    [JsonProperty(PropertyName = "translations")]
    public IList<TextValue> Translations { get; set; } = new List<TextValue>();

    internal class TextValue
    {
        [JsonProperty(PropertyName = "detected_source_language")]
        public string DetectedSourceLanguage { get; set; } = "";
        [JsonProperty(PropertyName = "text")]
        public string? Text { get; set; }
    }
}
