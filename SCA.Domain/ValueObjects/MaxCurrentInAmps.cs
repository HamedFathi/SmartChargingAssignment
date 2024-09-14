using HamedStack.TheAggregateRoot;

namespace SCA.Domain.ValueObjects;

public class MaxCurrentInAmps : SingleValueObject<int>
{
    public MaxCurrentInAmps(int value)
    {
        Value = value;
    }
}