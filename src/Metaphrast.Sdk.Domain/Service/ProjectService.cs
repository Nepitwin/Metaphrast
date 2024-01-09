using System.Globalization;
using Metaphrast.Sdk.Domain.Enum;
using Metaphrast.Sdk.Domain.Exception;
using Metaphrast.Sdk.Domain.Model;
using Metaphrast.Sdk.Domain.Port;

namespace Metaphrast.Sdk.Domain.Service;

public class ProjectService(IProjectRepository adapter): IProjectService
{
    public Guid Create(string name)
    {
        try
        {
            return adapter.Save(new Project(Guid.NewGuid(), name));
        }
        catch (MetaphrastDomainException)
        {
            throw;
        }
        catch (System.Exception)
        {
            throw new MetaphrastDomainException(ErrorDomainReason.EntityNotCreated);
        }
    }

    public void AddLanguage(Guid identifier, CultureInfo language)
    {
        try
        {
            var project = adapter.GetProjectById(identifier) ?? throw new MetaphrastDomainException(ErrorDomainReason.EntityNotFound);

            if (!project.AddLanguage(language))
            {
                throw new MetaphrastDomainException(ErrorDomainReason.EntityAlreadyExists);
            }

            adapter.Save(project);
        }
        catch (MetaphrastDomainException)
        {
            throw;
        }
        catch (System.Exception)
        {
            throw new MetaphrastDomainException(ErrorDomainReason.EntityNotUpdated);
        }
    }
}