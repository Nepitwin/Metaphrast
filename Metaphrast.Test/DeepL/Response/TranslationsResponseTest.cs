using System.Collections.Generic;
using Metaphrast.Sdk.DeepL.Response;
using Newtonsoft.Json;
using Xunit;

namespace Metaphrast.Test.DeepL.Response;

public class TranslationsTest
{

    [Fact]
    public void ValidResponseTest()
    {
        const string json = @"{'translations': [{'detected_source_language': 'EN', 'text': 'Niederlande'},{'detected_source_language': 'EN','text': 'Hallo, Welt!'}]}";
        var expectedValues = new List<string> { "Niederlande", "Hallo, Welt!" };

        var translations = JsonConvert.DeserializeObject<TranslationsResponse>(json);
        Assert.NotNull(translations);
        var i = 0;
        foreach (var translationText in translations?.Translations)
        {
            Assert.Equal("EN", translationText.DetectedSourceLanguage);
            Assert.Equal(expectedValues[i], translationText.Text);
            i++;
        }
    }

    [Fact]
    public void EmptyResponseTest()
    {
        const string json = @"{}";
        var translations = JsonConvert.DeserializeObject<TranslationsResponse>(json);
        Assert.NotNull(translations);
        Assert.NotNull(translations.Translations);
        Assert.Empty(translations.Translations);
    }

    [Fact]
    public void WrongResponseTest()
    {
        const string json = @"{'value':'value'}";
        var translations = JsonConvert.DeserializeObject<TranslationsResponse>(json);
        Assert.NotNull(translations);
        Assert.NotNull(translations.Translations);
        Assert.Empty(translations.Translations);
    }
}
