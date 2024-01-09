using System.Globalization;

namespace Metaphrast.Sdk.Domain.Port;

public interface IProjectService
{
    Guid Create(string name);
    void AddLanguage(Guid identifier, CultureInfo language);
}