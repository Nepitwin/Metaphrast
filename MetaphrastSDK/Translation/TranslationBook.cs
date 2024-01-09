using Metaphrast.Sdk.Crypto;
using Metaphrast.Sdk.DeepL.Parameters;
using Metaphrast.Sdk.IO;

namespace Metaphrast.Sdk.Translation;
internal class TranslationBook
{
    private readonly Glossary _sourceGlossary;
    private readonly Glossary _targetGlossary;
    // Stores all key hashes from translated target book by XOR operation
    private readonly Dictionary<string, string> _hashes;

    public TranslationBook(Glossary sourceGlossary, Glossary targetGlossary, Dictionary<string, string> hashes)
    {
        _sourceGlossary = sourceGlossary;
        _targetGlossary = targetGlossary;
        _hashes = hashes;
    }

    public Dictionary<string, string> GetModifiedTranslations()
    {
        var list = new Dictionary<string, string>();

        foreach (var text in _sourceGlossary.Texts)
        {
            if (_targetGlossary.Texts.ContainsKey(text.Key))
            {
                if (!_hashes.ContainsKey(text.Key) || IsHashModified(text.Value, _targetGlossary.Texts[text.Key], text.Key))
                {
                    list[text.Key] = text.Value;
                }
            }
            else
            {
                list[text.Key] = text.Value;
            }
        }

        return list;
    }

    public void SetTranslation(string key, string value)
    {
        if (!_sourceGlossary.Texts.ContainsKey(key))
        {
            throw new KeyNotFoundException();
        }

        _targetGlossary.Texts[key] = value;
        _hashes[key] = CalculateHash(value, _sourceGlossary.Texts[key]);
    }

    public bool ExistTranslationHash(string key)
    {
        return _hashes.ContainsKey(key);
    }

    public string GetTranslationHash(string key)
    {
        return ExistTranslationHash(key) ? _hashes[key] : "";
    }

    public Language GetSourceLanguage()
    {
        return _sourceGlossary.Language;
    }

    public Language GetTranslationLanguage()
    {
        return _targetGlossary.Language;
    }

    public void SaveHashesToFile(string file)
    {
        Json<Dictionary<string, string>>.Save(_hashes, file);
    }

    public void SaveTranslationsToFile(string file)
    {
        Json<Glossary>.Save(_targetGlossary, file);
    }

    private bool IsHashModified(string inputA, string inputB, string key)
    {
        return CalculateHash(inputA, inputB) != _hashes[key];
    }

    private static string CalculateHash(string inputA, string inputB)
    {
        return Algorithm.Sha256ToString(Algorithm.Sha256Xor(Algorithm.Sha256ToBytes(inputA), Algorithm.Sha256ToBytes(inputB)));
    }
}

