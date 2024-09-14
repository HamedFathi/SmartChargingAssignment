using HamedStack.CQRS;

namespace SCA.Application.Groups.Commands.Create;

public class CreateGroupCommand(string name, int capacity) : ICommand<Guid>
{
    public string Name { get; } = name;
    public int Capacity { get; } = capacity;
}