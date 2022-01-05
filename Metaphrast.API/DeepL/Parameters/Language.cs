namespace Metaphrast.DeepL.Parameters;

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
            CodeBulgarian => Bulgarian,
            CodeCzech => Czech,
            CodeDanish => Danish,
            CodeGerman => German,
            CodeGreek => Greek,
            CodeEnglishBritish => EnglishBritish,
            CodeEnglishAmerican => EnglishAmerican,
            CodeEnglish => English,
            CodeSpanish => Spanish,
            CodeEstonian => Estonian,
            CodeFinnish => Finnish,
            CodeFrench => French,
            CodHungarian => Hungarian,
            CodeItalian => Italian,
            CodeJapanese => Japanese,
            CodeLithuanian => Lithuanian,
            CodeLatvian => Latvian,
            CodeDutch => Dutch,
            CodePolish => Polish,
            CodPortuguesePortugal => PortuguesePortugal,
            CodePortugueseBrazil => PortugueseBrazil,
            CodePortuguese => Portuguese,
            CodeRomanian => Romanian,
            CodeRussian => Russian,
            CodeSlovak => Slovak,
            CodeSlovenian => Slovenian,
            CodeSwedish => Swedish,
            CodeChinese => Chinese,
            _ => throw new NotSupportedException("Language translation not supported"),
        };
    }

    private const string CodeBulgarian = "BG";
    private const string CodeCzech = "CS";
    private const string CodeDanish = "DA";
    private const string CodeGerman = "DE";
    private const string CodeGreek = "EL";
    private const string CodeEnglishBritish = "EN-GB";
    private const string CodeEnglishAmerican = "EN-US";
    private const string CodeEnglish = "EN";
    private const string CodeSpanish = "ES";
    private const string CodeEstonian = "ET";
    private const string CodeFinnish = "FI";
    private const string CodeFrench = "FR";
    private const string CodHungarian = "HU";
    private const string CodeItalian = "IT";
    private const string CodeJapanese = "JA";
    private const string CodeLithuanian = "LT";
    private const string CodeLatvian = "LV";
    private const string CodeDutch = "NL";
    private const string CodePolish = "PL";
    private const string CodPortuguesePortugal = "PT-PT";
    private const string CodePortugueseBrazil = "PT-BR";
    private const string CodePortuguese = "PT";
    private const string CodeRomanian = "RO";
    private const string CodeRussian = "RU";
    private const string CodeSlovak = "SK";
    private const string CodeSlovenian = "SL";
    private const string CodeSwedish = "SV";
    private const string CodeChinese = "ZH";

    public static readonly Language Bulgarian = new(CodeBulgarian);
    public static readonly Language Czech = new(CodeCzech);
    public static readonly Language Danish = new(CodeDanish);
    public static readonly Language German = new(CodeGerman);
    public static readonly Language Greek = new(CodeGreek);
    public static readonly Language EnglishBritish = new(CodeEnglishBritish);
    public static readonly Language EnglishAmerican = new(CodeEnglishAmerican);
    /**
     * (unspecified variant for backward compatibility; please select EN-GB or EN-US instead)
     */
    public static readonly Language English = new(CodeEnglish);
    public static readonly Language Spanish = new(CodeSpanish);
    public static readonly Language Estonian = new(CodeEstonian);
    public static readonly Language Finnish = new(CodeFinnish);
    public static readonly Language French = new(CodeFrench);
    public static readonly Language Hungarian = new(CodHungarian);
    public static readonly Language Italian = new(CodeItalian);
    public static readonly Language Japanese = new(CodeJapanese);
    public static readonly Language Lithuanian = new(CodeLithuanian);
    public static readonly Language Latvian = new(CodeLatvian);
    public static readonly Language Dutch = new(CodeDutch);
    public static readonly Language Polish = new(CodePolish);
    public static readonly Language PortuguesePortugal = new(CodPortuguesePortugal);
    public static readonly Language PortugueseBrazil = new(CodePortugueseBrazil);
    /**
     * Portuguese (unspecified variant for backward compatibility; please select PT-PT or PT-BR instead)
     */
    public static readonly Language Portuguese = new(CodePortuguese);
    public static readonly Language Romanian = new(CodeRomanian);
    public static readonly Language Russian = new(CodeRussian);
    public static readonly Language Slovak = new(CodeSlovak);
    public static readonly Language Slovenian = new(CodeSlovenian);
    public static readonly Language Swedish = new(CodeSwedish);
    public static readonly Language Chinese = new(CodeChinese);

    private readonly string _language;

    private Language(string language)
    {
        _language = language;
    }
}