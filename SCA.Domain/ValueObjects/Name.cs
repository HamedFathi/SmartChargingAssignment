using HamedStack.TheAggregateRoot;

namespace SCA.Domain.ValueObjects;

public class Name : SingleValueObject<string>
{
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.");

        if (value.Length > 100)
            throw new ArgumentException("Name cannot be longer than 100 characters.");

        Value = value;
    }
}