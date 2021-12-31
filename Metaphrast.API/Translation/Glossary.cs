using Metaphrast.DeepL;

namespace Metaphrast.Translation;
public class Glossary
{
    public string Language { get; }

    public Dictionary<string, Text> Texts { get; } = new();

    public Glossary(Language language)
    {
        Language = language;
    }

    public List<Text> GetTranslationTexts(Glossary glossary)
    {
        return new();
    }
}
