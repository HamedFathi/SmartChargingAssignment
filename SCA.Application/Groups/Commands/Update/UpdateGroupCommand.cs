using HamedStack.CQRS;

namespace SCA.Application.Groups.Commands.Update;

public class UpdateGroupCommand(Guid id, string? name, int? capacity) : ICommand<bool>
{
    public Guid Id { get; } = id;
    public string? Name { get; } = name;
    public int? Capacity { get; } = capacity;
}