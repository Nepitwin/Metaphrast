using Metaphrast.DeepL;
using Metaphrast.Translation;

namespace Metaphrast;

/**
 * DeepL API implementation by text translations
 * https://www.deepl.com/de/docs-api/translating-text
 */
public class API : IDisposable
{
    private readonly string _apiKey;

    public API(string apiKey)
    {
        _apiKey = apiKey;
    }

    public void Translate(Language sourceGlossaryLanguage, List<Glossary> glossaries)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
