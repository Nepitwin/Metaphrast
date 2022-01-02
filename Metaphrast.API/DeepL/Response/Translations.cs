﻿namespace Metaphrast.DeepL;

/**
 * JSON DeepL response from translation API.
 *
 * Input ["Netherlands", "Hello World"]
 *
 * Multiple german translation response example:
 * {
 *  'translations': [
 *      {
 *          'detected_source_language': 'EN',
 *          'text': 'Niederlande'
 *      },
 *      {
 *           'detected_source_language': 'EN',
 *          'text': 'Hallo, Welt!'
 *      }
 *  ]
 * }
 */
public sealed class Translations
{
    public IList<Text> translations { get; set; } = new List<Text>();

    public sealed class Text
    {
        public string detected_source_language { get; set; }
        public string text { get; set; }
    }
}
