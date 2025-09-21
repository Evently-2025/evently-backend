using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events;

public sealed class Event: Entity
{
    private Event() { }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public DateTime StartAtUtc { get; private set; }
    public DateTime? EndAtUtc { get; private set; }
    public EventStatus Status { get; private set; }

    public static Result<Event> Create(
        string Title,
        string Description,
        string Location,
        DateTime StartAtUtc,
        DateTime? EndAtUtc)
    {
        if (EndAtUtc.HasValue && EndAtUtc < StartAtUtc)
        {
            return Result.Failure<Event>(EventErrors.EndDatePrecedesStartDate);
        }

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = Title,
            Description = Description,
            Location = Location,
            StartAtUtc = StartAtUtc,
            EndAtUtc = EndAtUtc,
            Status = EventStatus.Draft
        };

        @event.Raise(new EventCreatedDomainEvent(@event.Id));

        return @event;
    }
}
