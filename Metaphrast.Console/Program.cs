using Metaphrast.DeepL;
using Metaphrast.Translation;

Console.WriteLine(Language.English);

// Create source glossary for all corresponding translations
var glossaryEn = new Glossary(Language.English);
glossaryEn.Texts.Add("Holland", new Text("Holland"));
glossaryEn.Texts.Add("England", new Text("England"));
glossaryEn.Texts.Add("Italy", new Text("Italy"));
glossaryEn.Texts.Add("Spain", new Text("Spain"));

// Create all glossaries for translation entities
var glossaryDe = new Glossary(Language.German);
var glossaryFr = new Glossary(Language.French);
var glossaryIt = new Glossary(Language.Italian);

/*
using var api = new API("API_KEY");
api.Translate(Language.English, new List<Glossary>
{
    glossaryEn,
    glossaryDe,
    glossaryFr,
    glossaryIt
});
*/
