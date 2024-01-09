using System.Collections.Generic;
using Metaphrast.Sdk.DeepL.Parameters;
using Metaphrast.Sdk.Translation;
using Xunit;

namespace Metaphrast.Test.Translation;

public class TranslationBookTest
{
    [Theory, MemberData(nameof(TranslationBookData))]
    public void GetTranslationsTest(
        List<string> sourceKeys, 
        List<string> sourceValues, 
        List<string> translationKeys, 
        List<string> translationValues,
        Dictionary<string, string> translationDictionary,
        Dictionary<string, string> expectedTranslations)
    {
        var glossary = new Glossary(Language.English);
        for(var i = 0; i < sourceKeys.Count; i++)
        {
            glossary.Texts.Add(sourceKeys[i], sourceValues[i]);
        }

        var translationGlossary = new Glossary(Language.German);
        if (translationKeys.Count > 0 && translationValues.Count > 0)
        {
            for (var i = 0; i < translationKeys.Count; i++)
            {
                translationGlossary.Texts.Add(translationKeys[i], translationValues[i]);
            }
        }

        var translationBook = new TranslationBook(glossary, translationGlossary, translationDictionary);
        var actualTranslations = translationBook.GetModifiedTranslations();
        foreach (var translation in expectedTranslations)
        {
            Assert.Equal(actualTranslations[translation.Key], translation.Value);
        }
        
    }

    [Fact]
    public void SetHashTest()
    {
        var sourceGlossary = new Glossary(Language.English);
        sourceGlossary.Texts.Add("Netherland", "Netherland");
        var translationGlossary = new Glossary(Language.German);
        var translationBook = new TranslationBook(sourceGlossary, translationGlossary, new Dictionary<string, string>());

        translationBook.SetTranslation("Netherland", "Niederlande");
        Assert.True(translationBook.ExistTranslationHash("Netherland"));
        Assert.Equal("00f181027313ba02cc0e237709f664674842306c21404263fd71ffab49d00d18", translationBook.GetTranslationHash("Netherland"));
    }

    [Fact]
    public void InvalidSetHashTest()
    {
        var sourceGlossary = new Glossary(Language.English);
        var translationGlossary = new Glossary(Language.German);
        var translationBook = new TranslationBook(sourceGlossary, translationGlossary, new Dictionary<string, string>());
        Assert.Throws<KeyNotFoundException>(() => translationBook.SetTranslation("Netherland", "Niederlande"));
    }

    public static IEnumerable<object[]> TranslationBookData =>
        new List<object[]>
        {
            new object[] 
            {
                new List<string>{"Netherland", "England", "Italy", "Spain"}, 
                new List<string>{"Holland", "England", "Italy", "Spain"} , 
                new List<string>(),
                new List<string>(), 
                new Dictionary<string, string>(),
                new Dictionary<string, string>                
                {
                    {"Netherland", "Holland" },
                    {"England", "England" },
                    {"Italy", "Italy" },
                    {"Spain", "Spain" },
                }
            },
            new object[]
            {
                new List<string>{"Netherland", "England", "Italy", "Spain"},
                new List<string>{"Holland", "England", "Italy", "Spain"} ,
                new List<string>{"Netherland"},
                new List<string>{"Niederlande"},
                new Dictionary<string, string>(),
                new Dictionary<string, string>
                {
                    {"Netherland", "Holland" },
                    {"England", "England" },
                    {"Italy", "Italy" },
                    {"Spain", "Spain" },
                }
            },
            new object[]
            {
                new List<string>{"Netherland", "England", "Italy", "Spain"},
                new List<string>{"Holland", "England", "Italy", "Spain"} ,
                new List<string>{"Netherland"},
                new List<string>{"Niederlande"},
                new Dictionary<string, string>
                {
                    {"Netherland", "5a9cf672c8be6b5ab9546a2fb49b06dd81a4e364c86ed023898c49d9bb0605dc" }
                },
                new Dictionary<string, string>
                {
                    {"Netherland", "Holland" },
                    {"England", "England" },
                    {"Italy", "Italy" },
                    {"Spain", "Spain" },
                }
            },
            new object[]
            {
                new List<string>{"Netherland", "England", "Italy", "Spain"},
                new List<string>{"Holland", "England", "Italy", "Spain"} ,
                new List<string>{"Netherland", "Italy"},
                new List<string>{"Niederlande", "Italien"},
                new Dictionary<string, string>
                {
                    {"Netherland", "e4174d695f9243a10d450c33342d6a2fbb27e1793591e0355081f05027c4a3b7" },
                    {"Italy", "b8a549e43270dccdaaa7024e76d3898be28afa6ece57ccd1bdb1040eb1d7763e" },
                    {"England", "c0ea960b065c6aa6dd567a1a741fa21aee3972692096e158cebb4309a3b2a716" },
                    {"Spain", "8ef41e6f4b07432a0cb4eb7a8774e7a3878fd3e385f49aa09b406768467db228" },

                },
                new Dictionary<string, string>
                {
                    {"England", "England" },
                    {"Spain", "Spain" },
                }
            },
        };
}
