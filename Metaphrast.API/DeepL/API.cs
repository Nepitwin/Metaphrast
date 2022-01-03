using Flurl;
using Flurl.Http;
using Metaphrast.DeepL;
using Metaphrast.Translation;
using Newtonsoft.Json;

namespace Metaphrast;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class API
{
    private readonly string _apiKey;
    private readonly string _apiUrl;

    public API(string apiKey, bool isFreeAccount)
    {
        _apiKey = apiKey;
        _apiUrl = isFreeAccount ? "https://api-free.deepl.com/v2/translate" : "https://api.deepl.com/v2/translate";
    }

    public void Translate(List<TranslationBook> translationBooks)
    {
        foreach (var book in translationBooks)
        {
            var translationList = book.GetTranslations();
            if (translationList.Count <= 0)
            {
                continue;
            }

            var resultHttpRequest = SendHttpRequest(book.SourceGlossary.Language, book.TargetGlossary.Language, new List<string>(translationList.Values));
            var translationsResult = JsonConvert.DeserializeObject<Translations>(resultHttpRequest.Result);

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
    
    private Task<string> SendHttpRequest(Language sourceLanguage, Language targetLanguage, IList<string> translationTexts)
    {
        return _apiUrl.SetQueryParam("text", translationTexts)
            .PostUrlEncodedAsync(new { target_lang = targetLanguage, source_lang = sourceLanguage, auth_key = _apiKey })
            .ReceiveString();
    }
}
