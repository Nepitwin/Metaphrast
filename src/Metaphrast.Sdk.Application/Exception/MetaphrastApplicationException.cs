using Metaphrast.Sdk.Application.Enum;

namespace Metaphrast.Sdk.Application.Exception;

public class MetaphrastApplicationException(ErrorApplicationReason reason) : System.Exception
{
    public ErrorApplicationReason Reason { get; private set; } = reason;
}
