using System.Collections.Generic;
using Metaphrast.DeepL;
using Newtonsoft.Json;
using Xunit;

namespace Metaphrast.Test.DeepL.Response;

public class TranslationsTest
{

    [Fact]
    public void Test()
    {
        // TODO
        string json = @"{'translations': [{'detected_source_language': 'EN', 'text': 'Niederlande'},{'detected_source_language': 'EN','text': 'Hallo, Welt!'}]}";
        Translations? translations = JsonConvert.DeserializeObject<Translations>(json);
        Assert.NotNull(translations);

        List<string> expectedValues = new List<string>();
        expectedValues.Add("Niederlande");
        expectedValues.Add("Hallo, Welt!");

        int i = 0;

        foreach (var translationText in translations.translations)
        {
            Assert.Equal("EN", translationText.detected_source_language);
            Assert.Equal(expectedValues[i], translationText.text);
            i++;
        }
    }

}