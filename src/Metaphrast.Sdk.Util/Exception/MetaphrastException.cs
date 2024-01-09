using Metaphrast.Sdk.Util.Enum;

namespace Metaphrast.Sdk.Util.Exception;

public class MetaphrastException(Reason reason) : System.Exception
{
    public Reason Reason { get; private set; } = reason;
}