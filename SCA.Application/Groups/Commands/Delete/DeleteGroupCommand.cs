using HamedStack.CQRS;

namespace SCA.Application.Groups.Commands.Delete;

public class DeleteGroupCommand(Guid id) : ICommand<bool>
{
    public Guid Id { get; } = id;
}