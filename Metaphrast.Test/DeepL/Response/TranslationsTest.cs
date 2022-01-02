using System.Collections.Generic;
using Metaphrast.DeepL;
using Newtonsoft.Json;
using Xunit;

namespace Metaphrast.Test.DeepL.Response;

public class TranslationsTest
{

    [Fact]
    public void ValidResponseTest()
    {
        var json =
            @"{'translations': [{'detected_source_language': 'EN', 'text': 'Niederlande'},{'detected_source_language': 'EN','text': 'Hallo, Welt!'}]}";

        var expectedValues = new List<string> { "Niederlande", "Hallo, Welt!" };

        Translations? translations = JsonConvert.DeserializeObject<Translations>(json);
        Assert.NotNull(translations);

        var i = 0;
        foreach (var translationText in translations?.translations)
        {
            Assert.Equal("EN", translationText.detected_source_language);
            Assert.Equal(expectedValues[i], translationText.text);
            i++;
        }
    }

    [Fact]
    public void EmptyResponseTest()
    {
        var json = @"{}";
        Translations? translations = JsonConvert.DeserializeObject<Translations>(json);
        Assert.NotNull(translations);
        Assert.NotNull(translations.translations);
        Assert.Empty(translations.translations);
    }

    [Fact]
    public void WrongResponseTest()
    {
        var json = @"{'value':'value'}";
        Translations? translations = JsonConvert.DeserializeObject<Translations>(json);
        Assert.NotNull(translations);
        Assert.NotNull(translations.translations);
        Assert.Empty(translations.translations);
    }
}
