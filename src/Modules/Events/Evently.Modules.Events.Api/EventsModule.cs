using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Api.Database;
using Evently.Modules.Events.Api.Events;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Api;
public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
    }

    public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<EventsDbContext>(options =>
            options.UseNpgsql(
                databaseConnectionString,
                npgsqlOptions =>
                {
                    // Keep the migrations history in Events schema
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schemas.Events);

                    // Retry if DB isn't ready yet
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,              // how many times to retry
                        maxRetryDelay: TimeSpan.FromSeconds(10), // delay between retries
                        errorCodesToAdd: null);        // or custom pg error codes
                })
            .UseSnakeCaseNamingConvention());

        return services;
    }

}
