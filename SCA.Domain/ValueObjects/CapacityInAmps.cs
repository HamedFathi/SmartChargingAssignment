using HamedStack.TheAggregateRoot;

namespace SCA.Domain.ValueObjects;

public class CapacityInAmps : SingleValueObject<int>
{
    public CapacityInAmps(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");

        Value = value;
    }
}