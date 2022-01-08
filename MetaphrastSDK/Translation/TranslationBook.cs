using MetaphrastSDK.Crypto;

namespace MetaphrastSDK.Translation;
internal class TranslationBook
{
    public Glossary SourceGlossary { get; }
    // ToDo Redesign
    public Glossary TargetGlossary { get; }
    // Stores all key hashes from translated target book by XOR operation
    public Dictionary<string, string> Hashes { get; }

    public TranslationBook(Glossary sourceGlossary, Glossary targetGlossary, Dictionary<string, string> hashes)
    {
        SourceGlossary = sourceGlossary;
        TargetGlossary = targetGlossary;
        Hashes = hashes;
    }

    public Dictionary<string, string> GetTranslations()
    {
        var list = new Dictionary<string, string>();

        foreach (var (key, value) in SourceGlossary.Texts)
        {
            if (TargetGlossary.Texts.ContainsKey(key))
            {
                if (!Hashes.ContainsKey(key) || IsHashModified(value, TargetGlossary.Texts[key], key))
                {
                    list[key] = value;
                }
            }
            else
            {
                list[key] = value;
            }
        }

        return list;
    }

    public void SetTranslation(string key, string value)
    {
        if (!SourceGlossary.Texts.ContainsKey(key))
        {
            throw new KeyNotFoundException();
        }

        TargetGlossary.Texts[key] = value;
        Hashes[key] = CalculateHash(value, SourceGlossary.Texts[key]);
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

