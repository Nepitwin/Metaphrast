using Metaphrast.Sdk.Domain.Enum;

namespace Metaphrast.Sdk.Domain.Exception;

public class MetaphrastDomainException(ErrorDomainReason reason) : System.Exception
{
    public ErrorDomainReason Reason { get; private set; } = reason;
}