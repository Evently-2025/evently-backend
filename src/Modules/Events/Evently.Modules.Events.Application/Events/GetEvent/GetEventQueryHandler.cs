using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql = 
            $"""
            SELECT
                id AS {nameof(EventResponse.Id)},
                title AS {nameof(EventResponse.Title)},
                description AS {nameof(EventResponse.Description)},
                location AS {nameof(EventResponse.Location)},
                start_at_utc AS {nameof(EventResponse.StartAtUtc)},
                end_at_utc AS {nameof(EventResponse.EndsAtUtc)}
            FROM events.events
            WHERE id = @EventId
            """;

        EventResponse? @event = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);
        return @event;
    }
}
