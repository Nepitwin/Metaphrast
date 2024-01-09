using Flurl;
using Flurl.Http;
using Metaphrast.Sdk.DeepL.Parameters;
using Metaphrast.Sdk.DeepL.Response;
using Metaphrast.Sdk.Error;
using Metaphrast.Sdk.Translation;
using Metaphrast.Sdk.Util;

namespace Metaphrast.Sdk.DeepL;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class Api
{
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private const int MaximumTextsInRequest = 50;

    public UsageResponse Usage { get; }

    public Api(string apiKey, bool isFreeAccount)
    {
        _apiKey = apiKey;
        _apiUrl = isFreeAccount ? "https://api-free.deepl.com/v2" : "https://api.deepl.com/v2";
        Usage = GetApiUsage().Result;
    }

    public async void Translate(List<TranslationBook> translationBooks)
    {
        foreach (var book in translationBooks)
        {
            var translationDict = book.GetModifiedTranslations();
            if (translationDict.Count <= 0)
            {
                continue;
            }

            var translationsResults = new List<IList<TranslationsResponse.TextValue>>();
            var splitValuesList = ListHelper.Split(new List<string>(translationDict.Values), MaximumTextsInRequest);
            var keyValuesList = ListHelper.Split(new List<string>(translationDict.Values), MaximumTextsInRequest);

            foreach (var list in splitValuesList)
            {
                var translationResponse = await SendTranslationRequestAsync(book.GetSourceLanguage(), book.GetTranslationLanguage(), list);
                translationsResults.Add(translationResponse.Translations);
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

    private Task<UsageResponse> GetApiUsage()
    {
        try
        {
            return $"{_apiUrl}/usage".SetQueryParam("auth_key", _apiKey).GetJsonAsync<UsageResponse>();
        }
        catch (FlurlHttpException ex)
        {
            if (ex.StatusCode == 403)
            {
                throw new MetaphrastException(MetaphrastExceptionType.INVALID_API_KEY, "Invalid Deepl-API-Key usage");
            }

            throw new MetaphrastException(MetaphrastExceptionType.UNKNOWN_ERROR, ex.Message);
        }
    }
    
    private Task<TranslationsResponse> SendTranslationRequestAsync(Language sourceLanguage, Language targetLanguage, List<string> translationTexts)
    {
        return $"{_apiUrl}/translate".SetQueryParam("text", translationTexts)
            .PostUrlEncodedAsync(new { target_lang = targetLanguage, source_lang = sourceLanguage, auth_key = _apiKey })
            .ReceiveJson<TranslationsResponse>();
    }
}
