using System.Globalization;
using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;

namespace Metaphrast.Sdk.Domain.Model;

/**
 * <summary>
 * Project entity class to manage a language translation project.
 * </summary>
 */
public class Project
{
    public Guid Identifier { get; }
    public string Name { get; }

    private readonly Dictionary<CultureInfo, HashSet<Translation>> _translations = [];

    public Project(Guid identifier, string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            throw new MetaphrastDomainException(ErrorDomainReason.ValidationError);
        }

        Identifier = identifier;
        Name = name;
    }

    /**
     * <summary>
     * Add translation to specified language.
     * </summary>
     *
     * <returns>
     * True if translation is added to language false if translation by identifier is already present.
     * </returns>
     */
    public bool AddTranslation(CultureInfo language, Translation translation)
    {
        if (ContainsLanguage(language))
        {
            return _translations[language].Add(translation);
        }

        _translations[language] = [translation];
        return true;
    }

    /**
     * <summary>
     * Add specified language.
     * </summary>
     *
     * <returns>
     * True if language is added false if language is already present.
     * </returns>
     */
    public bool AddLanguage(CultureInfo language)
    {
        if (ContainsLanguage(language))
        {
            return false;
        }

        _translations[language] = [];
        return true;
    }

    public bool UpdateTranslation(CultureInfo language, Translation updateTranslation)
    {
        throw new NotImplementedException();
    }

    public bool UpdateLanguage(CultureInfo language, CultureInfo changedLanguage)
    {
        throw new NotImplementedException();
    }

    public bool DeleteTranslation(CultureInfo language, Translation deleteTranslation)
    {
        throw new NotImplementedException();
    }

    public bool DeleteLanguage(CultureInfo language)
    {
        throw new NotImplementedException();
    }

    /**
     * <summary>
     * Checks if translation exists by given language
     * </summary>
     *
     * <returns>
     * True if translation exists in language otherwise False.
     * </returns>
     */
    public bool ContainsTranslation(CultureInfo language, Translation translation)
    {
        return _translations.TryGetValue(language, out var translations) && translations.Contains(translation);
    }

    /**
     * <summary>
     * Checks if language translation exists.
     * </summary>
     *
     * <returns>
     * True if language exists in otherwise False.
     * </returns>
     */
    public bool ContainsLanguage(CultureInfo language)
    {
        return _translations.ContainsKey(language);
    }
}