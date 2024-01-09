using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;

namespace Metaphrast.Sdk.Domain.Model;

/**
 * <summary>
 * Translation text entity which contains an identifier and text for a specific language.
 * </summary>
 */
public class Translation
{
    public string Identifier { get; private set; }
    public string Text { get; private set; }

    public Translation(string identifier, string text)
    {
        if (string.IsNullOrEmpty(identifier) || string.IsNullOrWhiteSpace(identifier))
        {
            throw new MetaphrastDomainException(ErrorDomainReason.ValidationError);
        }

        Identifier = identifier;
        Text = text;
    }
}