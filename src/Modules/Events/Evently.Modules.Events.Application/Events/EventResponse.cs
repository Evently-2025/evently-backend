namespace Evently.Modules.Events.Application.Events;

public sealed record EventResponse(
    Guid Id, 
    string Title, 
    string Description, 
    string Location, 
    DateTime StartAtUtc, 
    DateTime? EndsAtUtc
    );
