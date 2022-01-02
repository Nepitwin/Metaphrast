﻿using Metaphrast.DeepL;

namespace Metaphrast.Translation;
internal class Glossary
{
    public string Language { get; }

    public Dictionary<string, string> Texts { get; } = new();

    public Glossary(Language language)
    {
        Language = language;
    }
}
