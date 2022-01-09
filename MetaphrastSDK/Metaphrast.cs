using MetaphrastSDK.DeepL;
using MetaphrastSDK.IO;
using MetaphrastSDK.Translation;

namespace MetaphrastSDK;
public class Metaphrast
{
    private readonly Config.Config _config;
    private readonly List<TranslationBook> _books = new();
    private readonly Api _api;

    public Metaphrast(string configFile)
    {
        _config = Json<Config.Config>.Load(configFile);
        var sourceGlossary = Json<Glossary>.Load(_config.SourceTranslationFile);

        var filename = Path.GetFileNameWithoutExtension(_config.SourceTranslationFile);
        foreach (var translationLanguage in _config.TranslationLanguages)
        {
            var targetGlossaryFile = TranslationJsonFile(filename, translationLanguage);
            var targetGlossary = File.Exists(targetGlossaryFile) ? Json<Glossary>.Load(targetGlossaryFile) : new Glossary(translationLanguage);

            var hashGlossaryFile = TranslationHashJsonFile(filename, translationLanguage);
            var hashDictionary = File.Exists(hashGlossaryFile) ? Json<Dictionary<string, string>>.Load(hashGlossaryFile) : new Dictionary<string, string>();

            _books.Add(new TranslationBook(sourceGlossary, targetGlossary, hashDictionary));
        }

        _api = new Api(_config.Key, true);
    }

    public void Translate()
    {
        _api.Translate(_books);
    }

    public void Save()
    {
        var filename = Path.GetFileNameWithoutExtension(_config.SourceTranslationFile);
        foreach (var book in _books)
        {
            Save(book.Hashes, TranslationHashJsonFile(filename, book.TargetGlossary.Language.ToLower()));
            Save(book.TargetGlossary, TranslationJsonFile(filename, book.TargetGlossary.Language.ToLower()));
        }
    }

    public Tuple<long, long> GetUsage()
    {
        return new Tuple<long,long>(_api.Usage.CharacterCount, _api.Usage.CharacterLimit);
    }

    private static void Save<T>(T values, string file)
    {
        Json<T>.Save(values, file);
    }

    private static string TranslationJsonFile(string filename, string translationLanguage)
    {
        return filename + "_" + translationLanguage + ".json";
    }

    private static string TranslationHashJsonFile(string filename, string translationLanguage)
    {
        return filename + "_hash_" + translationLanguage + ".json";
    }

}
