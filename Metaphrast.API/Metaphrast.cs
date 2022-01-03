﻿using Metaphrast.Console.Config;
using Metaphrast.IO;
using Metaphrast.Translation;

namespace Metaphrast;
public class Metaphrast
{
    private readonly Config _config;
    private readonly List<TranslationBook> _books = new();
    private readonly API _api;

    public Metaphrast(string configFile)
    {
        _config = Json<Config>.Load(configFile);
        var sourceGlossary = Json<Glossary>.Load(_config.source_translation_file);

        var filename = Path.GetFileNameWithoutExtension(_config.source_translation_file);
        foreach (var translationLanguage in _config.translation_languages)
        {
            // ToDo Redesign
            var targetGlossaryFile = filename + "_" + translationLanguage + ".json";
            var hashGlossaryFile = filename + "_hash_" + translationLanguage + ".json";
            var targetGlossary = File.Exists(targetGlossaryFile) ? Json<Glossary>.Load(targetGlossaryFile) : new Glossary(translationLanguage);
            var hashDictionary = File.Exists(hashGlossaryFile) ? Json<Dictionary<string, string>>.Load(hashGlossaryFile) : new Dictionary<string, string>();
            _books.Add(new TranslationBook(sourceGlossary, targetGlossary, hashDictionary));
        }

        _api = new API(_config.api_key, true);
    }

    public void Translate()
    {
        _api.Translate(_books);
    }

    public void Save()
    {
        var filename = Path.GetFileNameWithoutExtension(_config.source_translation_file);
        foreach (var book in _books)
        {
            // ToDo Redesign
            var targetGlossaryFile = filename + "_" + book.TargetGlossary.Language.ToLower() + ".json";
            var hashGlossaryFile = filename + "_hash_" + book.TargetGlossary.Language.ToLower() + ".json";
            Json<Dictionary<string, string>>.Save(book.Hashes, hashGlossaryFile);
            Json<Glossary>.Save(book.TargetGlossary, targetGlossaryFile);
        }
    }
}
