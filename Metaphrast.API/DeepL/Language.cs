namespace Metaphrast.DeepL;

/**
 * DeepL argument implementation by specific text translations from source_lang or target_lang.
 * https://www.deepl.com/de/docs-api/translating-text
 */
public sealed class Language
{
    public static implicit operator string(Language language) => language.ToString();
    public override string ToString()
    {
        return _language;
    }

#pragma warning disable CA2211
    public static Language Bulgarian = new("BG");
    public static Language Czech = new("CS");
    public static Language Danish = new("DA");
    public static Language German = new("DE");
    public static Language Greek = new("EL");
    public static Language EnglishBritish = new("EN-GB");
    public static Language EnglishAmerican = new("EN-US");
    /**
     * (unspecified variant for backward compatibility; please select EN-GB or EN-US instead)
     */
    public static Language English = new("EN");
    public static Language Spanish = new("ES");
    public static Language Estonian = new("ET");
    public static Language Finnish = new("FI");
    public static Language French = new("FR");
    public static Language Hungarian = new("HU");
    public static Language Italian = new("IT");
    public static Language Japanese = new("JA");
    public static Language Lithuanian = new("LT");
    public static Language Latvian = new("LV");
    public static Language Dutch = new("NL");
    public static Language Polish = new("PL");
    public static Language PortuguesePortugal = new("PT-PT");
    public static Language PortugueseBrazil = new("PT-BR");
    /**
     * Portuguese (unspecified variant for backward compatibility; please select PT-PT or PT-BR instead)
     */
    public static Language Portuguese = new("PT");
    public static Language Romanian = new("RO");
    public static Language Russian = new("RU");
    public static Language Slovak = new("SK");
    public static Language Slovenian = new("SL");
    public static Language Swedish = new("SV");
    public static Language Chinese = new("ZH");
#pragma warning restore CA2211

    private readonly string _language;

    private Language(string language)
    {
        _language = language;
    }
}