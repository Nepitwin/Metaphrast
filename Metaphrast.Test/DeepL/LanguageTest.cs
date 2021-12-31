using System.Collections.Generic;
using Metaphrast.DeepL;
using Xunit;

namespace Metaphrast.Test.DeepL;
public class LanguageTest
{
    [Theory, MemberData(nameof(Languages))]
    public void LanguageCodeImplicitStringCastTest(string language, string expectedCode)
    {
        Assert.Equal(expectedCode, language);
    }

    [Theory, MemberData(nameof(Languages))]
    public void LanguageCodeToStringTest(Language language, string expectedCode)
    {
        Assert.Equal(expectedCode, language);
    }

    public static IEnumerable<object[]> Languages =>
        new List<object[]>
        {
            new object[] {Language.Bulgarian, "BG"},
            new object[] {Language.Czech, "CS"},
            new object[] {Language.Danish, "DA"},
            new object[] {Language.German, "DE"},
            new object[] {Language.Greek, "EL"},
            new object[] {Language.EnglishBritish, "EN-GB"},
            new object[] {Language.EnglishAmerican, "EN-US"},
            new object[] {Language.English, "EN"},
            new object[] {Language.Spanish, "ES"},
            new object[] {Language.Estonian, "ET"},
            new object[] {Language.Finnish, "FI"},
            new object[] {Language.French, "FR"},
            new object[] {Language.Hungarian, "HU"},
            new object[] {Language.Italian, "IT"},
            new object[] {Language.Japanese, "JA"},
            new object[] {Language.Lithuanian, "LT"},
            new object[] {Language.Latvian, "LV"},
            new object[] {Language.Dutch, "NL"},
            new object[] {Language.Polish, "PL"},
            new object[] {Language.PortuguesePortugal, "PT-PT"},
            new object[] {Language.PortugueseBrazil, "PT-BR"},
            new object[] {Language.Portuguese, "PT"},
            new object[] {Language.Romanian, "RO"},
            new object[] {Language.Russian, "RU"},
            new object[] {Language.Slovak, "SK"},
            new object[] {Language.Slovenian, "SL"},
            new object[] {Language.Swedish, "SV"},
            new object[] {Language.Chinese, "ZH"},
        };
}
