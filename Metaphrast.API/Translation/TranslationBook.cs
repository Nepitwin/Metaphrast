using Metaphrast.Crypto;

namespace Metaphrast.Translation;
public class TranslationBook
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

    public IList<string> GetTranslationList()
    {
        var list = new List<string>();

        foreach (var (key, value) in _sourceGlossary.Texts)
        {
            if (_targetGlossary.Texts.ContainsKey(key))
            {
                if (IsHashModified(value, _targetGlossary.Texts[key], key))
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

    public void AddTranslation(string key, string value)
    {
        _targetGlossary.Texts[key] = value;
        _hashes[key] = CalculateHash(value, _sourceGlossary.Texts[key]);
    }

    private bool IsHashModified(string inputA, string inputB, string key)
    {
        return !_hashes.ContainsKey(key) || CalculateHash(inputA, inputB) != _hashes[key];
    }

    private static string CalculateHash(string inputA, string inputB)
    {
        return Algorithm.Sha256ToString(Algorithm.Sha256Xor(Algorithm.Sha256ToBytes(inputA), Algorithm.Sha256ToBytes(inputB)));
    }
}

