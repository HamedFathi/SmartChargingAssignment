using HamedStack.CQRS;

namespace SCA.Application.Groups.Commands.Update;

public class UpdateGroupCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
    public required string? Name { get; init; }
    public required int? Capacity { get; init; }
    public required Guid Version { get; init; }
}