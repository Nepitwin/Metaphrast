using Flurl;
using Flurl.Http;
using Metaphrast.DeepL;
using Metaphrast.Translation;
using Metaphrast.Util;
using Newtonsoft.Json;

namespace Metaphrast;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class Api
{
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private const int MaximumTextsInRequest = 50;

    public Api(string apiKey, bool isFreeAccount)
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

            var splitLists = ListHelper.Split(new List<string>(translationList.Values), MaximumTextsInRequest);
            foreach (var list in splitLists)
            {
                var resultHttpRequest = SendHttpRequest(book.SourceGlossary.Language, book.TargetGlossary.Language, list);
                var translationsResult = JsonConvert.DeserializeObject<TranslationsResponse>(resultHttpRequest.Result);

                var translations = translationsResult?.Translations;
                if (translations == null)
                {
                    continue;
                }

                var i = 0;
                foreach (var translation in translationList)
                {
                    book.SetTranslation(translation.Key, translations[i].Text);
                    i++;
                }
            }
        }
    }
    
    private Task<string> SendHttpRequest(Language sourceLanguage, Language targetLanguage, List<string> translationTexts)
    {
        return _apiUrl.SetQueryParam("text", translationTexts)
            .PostUrlEncodedAsync(new { target_lang = targetLanguage, source_lang = sourceLanguage, auth_key = _apiKey })
            .ReceiveString();
    }
}
