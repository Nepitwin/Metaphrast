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
            var translationDict = book.GetTranslations();
            if (translationDict.Count <= 0)
            {
                continue;
            }

            var translationsResults = new List<IList<TranslationsResponse.TextValue>>();
            var splitValuesList = ListHelper.Split(new List<string>(translationDict.Values), MaximumTextsInRequest);
            var keyValuesList = ListHelper.Split(new List<string>(translationDict.Values), MaximumTextsInRequest);

            foreach (var list in splitValuesList)
            {
                var resultHttpRequest = SendHttpRequest(book.SourceGlossary.Language, book.TargetGlossary.Language, list);
                var translationResponse = JsonConvert.DeserializeObject<TranslationsResponse>(resultHttpRequest.Result);
                translationsResults.Add(translationResponse?.Translations);
            }

            var z = 0;
            foreach (var translationResult in translationsResults)
            {
                for (var i = 0; i < translationResult.Count; i++)
                {
                    book.SetTranslation(keyValuesList[z][i], translationResult[i].Text);
                }

                z++;
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
