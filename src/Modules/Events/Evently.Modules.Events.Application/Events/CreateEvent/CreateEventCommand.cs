using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    string Title,
    string Description,
    string Location,
    DateTime StartAtUtc,
    DateTime? EndAtUtc) : ICommand<Guid>;
