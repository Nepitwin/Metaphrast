using System.Collections.Generic;
using MetaphrastSDK.DeepL.Parameters;
using MetaphrastSDK.Translation;
using Newtonsoft.Json;
using Xunit;

namespace Metaphrast.Test.Translation;
public class GlossaryTest
{

    [Theory, MemberData(nameof(TestData))]
    public void JsonTest(Dictionary<string, string> texts, string expectedJson)
    {
        var glossary = new Glossary(Language.Polish);
        foreach (var (key, value) in texts)
        {
            glossary.Texts.Add(key, value);
        }

        Assert.Equal(expectedJson, JsonConvert.SerializeObject(glossary));
    }

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[]
            {
                new Dictionary<string, string>(),
                @"{""Language"":""PL"",""Texts"":{}}"
            },
            new object[]
            {
                new Dictionary<string, string>{{"Hello", "World"}, {"Key", "Value"}},
                @"{""Language"":""PL"",""Texts"":{""Hello"":""World"",""Key"":""Value""}}"
            },
        };
}
