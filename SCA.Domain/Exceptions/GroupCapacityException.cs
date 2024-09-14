namespace SCA.Domain.Exceptions;

public class GroupCapacityException : DomainException
{
    public GroupCapacityException()
    {
    }

    public GroupCapacityException(string message)
        : base(message)
    {
    }

    public GroupCapacityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}