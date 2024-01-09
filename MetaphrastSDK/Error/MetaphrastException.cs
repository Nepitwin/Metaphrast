// ReSharper disable once CheckNamespace
namespace Metaphrast.Sdk.Error;

public enum MetaphrastExceptionType
{
    UNKNOWN_ERROR,
    INVALID_API_KEY,
}

public class MetaphrastException : Exception
{

    public MetaphrastExceptionType ExceptionType { get; }

    public MetaphrastException(MetaphrastExceptionType exceptionType, string message) : base(message)
    {
        ExceptionType = exceptionType;
    }
}