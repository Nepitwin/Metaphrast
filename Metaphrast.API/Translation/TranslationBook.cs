using Metaphrast.Crypto;

namespace Metaphrast.Translation;
public class TranslationBook
{
    private readonly Glossary _sourceGlossary;
    private readonly Glossary _targetGlossary;
    // Stores all key hashes from translated target book by XOR operation
    public Dictionary<string, string> Hashes { get; }

    public TranslationBook(Glossary sourceGlossary, Glossary targetGlossary, Dictionary<string, string> hashes)
    {
        _sourceGlossary = sourceGlossary;
        _targetGlossary = targetGlossary;
        Hashes = hashes;
    }

    public IList<string> GetTranslations()
    {
        var list = new List<string>();

        foreach (var (key, value) in _sourceGlossary.Texts)
        {
            if (_targetGlossary.Texts.ContainsKey(key))
            {
                if (!Hashes.ContainsKey(key) || IsHashModified(value, _targetGlossary.Texts[key], key))
                {
                    list.Add(value);
                }
            }
            else
            {
                list.Add(value);
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
        Hashes[key] = CalculateHash(value, _sourceGlossary.Texts[key]);
    }

    private bool IsHashModified(string inputA, string inputB, string key)
    {
        return CalculateHash(inputA, inputB) != Hashes[key];
    }

    private static string CalculateHash(string inputA, string inputB)
    {
        return Algorithm.Sha256ToString(Algorithm.Sha256Xor(Algorithm.Sha256ToBytes(inputA), Algorithm.Sha256ToBytes(inputB)));
    }
}

