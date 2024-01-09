namespace Metaphrast.Sdk.Domain.Enum;

public enum ErrorDomainReason
{
    EntityInvalidOperation,
    EntityAlreadyExists,
    EntityNotFound, 
    EntityNotCreated,
    EntityNotUpdated,
    ValidationError
}