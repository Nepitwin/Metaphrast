using Metaphrast;
using Metaphrast.Crypto;
using Metaphrast.DeepL;
using Metaphrast.Translation;
using Newtonsoft.Json;

Console.WriteLine(Language.English);

// Create source glossary for all corresponding translations
var glossaryEn = new Glossary(Language.English);
glossaryEn.Texts.Add("Holland", "Holland");
glossaryEn.Texts.Add("England", "England");
glossaryEn.Texts.Add("Italy", "Italy");
glossaryEn.Texts.Add("Spain", "Spain");


Console.WriteLine(JsonConvert.SerializeObject(glossaryEn));

string json =
    "{'Language':'EN','Texts':{'Holland':'Holland','England':'England','Italy':'Italy','Spain':'Spain'}}";

var gloss = JsonConvert.DeserializeObject<Glossary>(json);
Console.WriteLine(gloss);

Dictionary<string, byte[]> hashes = new();
hashes.Add("Not", Algorithm.Sha256ToBytes("England"));
Console.WriteLine(JsonConvert.SerializeObject(hashes));



/*
// Create glossaries by specific language and create a translation book
var bookDe = new TranslationBook(glossaryEn, new Glossary(Language.German));
var bookFr = new TranslationBook(glossaryEn, new Glossary(Language.French));
var bookIt = new TranslationBook(glossaryEn, new Glossary(Language.Italian));

using var api = new API("API_KEY");
api.Translate(new List<TranslationBook>
{
    bookDe,
    bookFr,
    bookIt
});

bookDe.Save("bookDe");
bookFr.Save("bookFr");
bookIt.Save("bookIt");
*/