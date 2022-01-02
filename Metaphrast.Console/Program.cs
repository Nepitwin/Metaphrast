using Metaphrast;
using Metaphrast.DeepL;
using Metaphrast.Translation;

// ToDo := Implementation from Program
// By default load config.json
// Contains information about source translation file
// API Token
// Selected language translations
// Verify configuration
// Call Deepl-API
// Store all glossary translations + hashes in corresponding json files

// Create source glossary for all corresponding translations
var glossaryEn = new Glossary(Language.English);
glossaryEn.Texts.Add("Holland", "Holland");
glossaryEn.Texts.Add("England", "England");
glossaryEn.Texts.Add("Italy", "Italy");
glossaryEn.Texts.Add("Spain", "Spain");

// Create glossaries by specific language and create a translation book
var bookDe = new TranslationBook(glossaryEn, new Glossary(Language.German), new Dictionary<string, string>());
var bookFr = new TranslationBook(glossaryEn, new Glossary(Language.French), new Dictionary<string, string>());
var bookIt = new TranslationBook(glossaryEn, new Glossary(Language.Italian), new Dictionary<string, string>());

using var api = new API("API_KEY");
api.Translate(new List<TranslationBook>
{
    bookDe,
    bookFr,
    bookIt
});
