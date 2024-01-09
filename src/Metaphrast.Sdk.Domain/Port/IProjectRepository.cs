using Metaphrast.Sdk.Domain.Model;

namespace Metaphrast.Sdk.Domain.Port;

public interface IProjectRepository
{
    Project? GetProjectById(Guid id);
    Guid Save(Project project);
}