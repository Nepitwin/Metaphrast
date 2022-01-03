namespace Metaphrast.DeepL;

/**
 * DeepL argument implementation by specific text translations from source_lang or target_lang.
 * https://www.deepl.com/de/docs-api/translating-text
 */
internal class Language
{
    public static implicit operator string(Language language) => language.ToString();

    public static implicit operator Language(string language) => ConvertFromString(language);
    
    public override string ToString()
    {
        return _language;
    }

    private static Language ConvertFromString(string language)
    {
        language = language.ToUpper();
        return language switch
        {
            ApiCodeBulgarian => Bulgarian,
            ApiCodeCzech => Czech,
            ApiCodeDanish => Danish,
            ApiCodeGerman => German,
            ApiCodeGreek => Greek,
            ApiCodeEnglishBritish => EnglishBritish,
            ApiCodeEnglishAmerican => EnglishAmerican,
            ApiCodeEnglish => English,
            ApiCodeSpanish => Spanish,
            ApiCodeEstonian => Estonian,
            ApiCodeFinnish => Finnish,
            ApiCodeFrench => French,
            ApiCodHungarian => Hungarian,
            ApiCodeItalian => Italian,
            ApiCodeJapanese => Japanese,
            ApiCodeLithuanian => Lithuanian,
            ApiCodeLatvian => Latvian,
            ApiCodeDutch => Dutch,
            ApiCodePolish => Polish,
            ApiCodPortuguesePortugal => PortuguesePortugal,
            ApiCodePortugueseBrazil => PortugueseBrazil,
            ApiCodePortuguese => Portuguese,
            ApiCodeRomanian => Romanian,
            ApiCodeRussian => Russian,
            ApiCodeSlovak => Slovak,
            ApiCodeSlovenian => Slovenian,
            ApiCodeSwedish => Swedish,
            ApiCodeChinese => Chinese,
            _ => throw new NotSupportedException("Language translation not supported"),
        };
    }

#pragma warning disable CA2211
    private const string ApiCodeBulgarian = "BG";
    private const string ApiCodeCzech = "CS";
    private const string ApiCodeDanish = "DA";
    private const string ApiCodeGerman = "DE";
    private const string ApiCodeGreek = "EL";
    private const string ApiCodeEnglishBritish = "EN-GB";
    private const string ApiCodeEnglishAmerican = "EN-US";
    private const string ApiCodeEnglish = "EN";
    private const string ApiCodeSpanish = "ES";
    private const string ApiCodeEstonian = "ET";
    private const string ApiCodeFinnish = "FI";
    private const string ApiCodeFrench = "FR";
    private const string ApiCodHungarian = "HU";
    private const string ApiCodeItalian = "IT";
    private const string ApiCodeJapanese = "JA";
    private const string ApiCodeLithuanian = "LT";
    private const string ApiCodeLatvian = "LV";
    private const string ApiCodeDutch = "NL";
    private const string ApiCodePolish = "PL";
    private const string ApiCodPortuguesePortugal = "PT-PT";
    private const string ApiCodePortugueseBrazil = "PT-BR";
    private const string ApiCodePortuguese = "PT";
    private const string ApiCodeRomanian = "RO";
    private const string ApiCodeRussian = "RU";
    private const string ApiCodeSlovak = "SK";
    private const string ApiCodeSlovenian = "SL";
    private const string ApiCodeSwedish = "SV";
    private const string ApiCodeChinese = "ZH";

    public static Language Bulgarian = new(ApiCodeBulgarian);
    public static Language Czech = new(ApiCodeCzech);
    public static Language Danish = new(ApiCodeDanish);
    public static Language German = new(ApiCodeGerman);
    public static Language Greek = new(ApiCodeGreek);
    public static Language EnglishBritish = new(ApiCodeEnglishBritish);
    public static Language EnglishAmerican = new(ApiCodeEnglishAmerican);
    /**
     * (unspecified variant for backward compatibility; please select EN-GB or EN-US instead)
     */
    public static Language English = new(ApiCodeEnglish);
    public static Language Spanish = new(ApiCodeSpanish);
    public static Language Estonian = new(ApiCodeEstonian);
    public static Language Finnish = new(ApiCodeFinnish);
    public static Language French = new(ApiCodeFrench);
    public static Language Hungarian = new(ApiCodHungarian);
    public static Language Italian = new(ApiCodeItalian);
    public static Language Japanese = new(ApiCodeJapanese);
    public static Language Lithuanian = new(ApiCodeLithuanian);
    public static Language Latvian = new(ApiCodeLatvian);
    public static Language Dutch = new(ApiCodeDutch);
    public static Language Polish = new(ApiCodePolish);
    public static Language PortuguesePortugal = new(ApiCodPortuguesePortugal);
    public static Language PortugueseBrazil = new(ApiCodePortugueseBrazil);
    /**
     * Portuguese (unspecified variant for backward compatibility; please select PT-PT or PT-BR instead)
     */
    public static Language Portuguese = new(ApiCodePortuguese);
    public static Language Romanian = new(ApiCodeRomanian);
    public static Language Russian = new(ApiCodeRussian);
    public static Language Slovak = new(ApiCodeSlovak);
    public static Language Slovenian = new(ApiCodeSlovenian);
    public static Language Swedish = new(ApiCodeSwedish);
    public static Language Chinese = new(ApiCodeChinese);
#pragma warning restore CA2211

    private readonly string _language;

    private Language(string language)
    {
        _language = language;
    }
}