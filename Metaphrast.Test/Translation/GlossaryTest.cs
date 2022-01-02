using System.Collections.Generic;
using Metaphrast.DeepL;
using Metaphrast.Translation;
using Newtonsoft.Json;
using Xunit;

namespace Metaphrast.Test.Translation;
public class GlossaryTest
{
    [Theory, MemberData(nameof(GlossaryData))]
    public void JsonTest(object glossary, Dictionary<string, string> texts, string expectedJson)
    {
        foreach (var (key, value) in texts)
        {
            ((Glossary)glossary).Texts.Add(key, value);
        }

        Assert.Equal(expectedJson, JsonConvert.SerializeObject(glossary));
    }

    public static IEnumerable<object[]> GlossaryData =>
        new List<object[]>
        {
            new object[]
            {
                new Glossary(Language.Polish),
                new Dictionary<string, string>(),
                @"{""Language"":""PL"",""Texts"":{}}"
            },
            new object[]
            {
                new Glossary(Language.Polish),
                new Dictionary<string, string>{{"Hello", "World"}, {"Key", "Value"}},
                @"{""Language"":""PL"",""Texts"":{""Hello"":""World"",""Key"":""Value""}}"
            },
        };
}
