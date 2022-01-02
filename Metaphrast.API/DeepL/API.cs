using Metaphrast.DeepL;
using Metaphrast.Translation;
using Newtonsoft.Json;

namespace Metaphrast;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class API : IDisposable
{
    private readonly string _apiKey;

    public API(string apiKey)
    {
        _apiKey = apiKey;
    }

    public void Translate(List<TranslationBook> translationBooks)
    {
        /**

        Configuration
        {
          "api" : "api_key",
          "source" : "translation.json",
          "languages" : ["DE", "FR", "IT"]
        }

        Corresponding Translation file

        {"Language":"EN","Texts":{"Netherlands":"Netherlands","England":"England","Italy":"Italy","Spain":"Spain"}}
         */

        var fakeResult =
            @"{'translations': [{'detected_source_language': 'EN', 'text': 'Niederlande'},{'detected_source_language': 'EN','text': 'England'},{'detected_source_language': 'EN','text': 'Italien'},{'detected_source_language': 'EN','text': 'Spanien'}]}";

        foreach (var book in translationBooks)
        {
            var translationList = book.GetTranslations();
            if (translationList.Count <= 0)
            {
                continue;
            }

            // ToDo Send HTTP Request

            var translationsResult = JsonConvert.DeserializeObject<Translations>(fakeResult);

            var i = 0;
            var translations = translationsResult?.translations;
            if (translations == null)
            {
                continue;
            }
            
            foreach (var translation in translationList)
            {
                book.SetTranslation(translation.Key, translations[i].text);
                i++;
            }
        }
    }

    public void Dispose()
    {

    }
}
