using Flurl;
using Flurl.Http;
using MetaphrastSDK.DeepL.Parameters;
using MetaphrastSDK.DeepL.Response;
using MetaphrastSDK.Error;
using MetaphrastSDK.Translation;
using MetaphrastSDK.Util;

namespace MetaphrastSDK.DeepL;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class Api
{
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private const int MaximumTextsInRequest = 50;

    public UsageResponse Usage { get; private set; }

    public Api(string apiKey, bool isFreeAccount)
    {
        _apiKey = apiKey;
        _apiUrl = isFreeAccount ? "https://api-free.deepl.com/v2" : "https://api.deepl.com/v2";
        GetApiUsage();
    }

    public void GetApiUsage()
    {
        try
        {
            Usage = SendUsageRequest();
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

    public void Translate(List<TranslationBook> translationBooks)
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
                var translationResponse = SendTranslationRequest(book.GetSourceLanguage(), book.GetTranslationLanguage(), list);
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

    private UsageResponse SendUsageRequest()
    {
        // ToDo Async/Await Usage
        var usageUrl = _apiUrl + "/usage";
        return usageUrl.SetQueryParam("auth_key", _apiKey).GetJsonAsync<UsageResponse>().GetAwaiter().GetResult();
    }
    
    private TranslationsResponse SendTranslationRequest(Language sourceLanguage, Language targetLanguage, List<string> translationTexts)
    {
        // ToDo Async/Await Usage
        var translationUrl = _apiUrl + "/translate";
        return translationUrl.SetQueryParam("text", translationTexts)
            .PostUrlEncodedAsync(new { target_lang = targetLanguage, source_lang = sourceLanguage, auth_key = _apiKey })
            .ReceiveJson<TranslationsResponse>().GetAwaiter().GetResult();
    }
}
