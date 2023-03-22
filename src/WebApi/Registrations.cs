using ClientModule;
using ClientModule.DomainEvents;
using Eventuous;
using Eventuous.EventStore;
using Eventuous.Spyglass;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using OrganizationModule;
using OrganizationModule.DomainEvents;
using System.Text.Json;

namespace WebApi;

public static class Registrations
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultEventSerializer
            .SetDefaultSerializer(
                new DefaultEventSerializer(
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                        .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)));

        services
            .AddEventStoreDb(configuration)
            .AddTelemetry();

        return services;
    }

    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddEventStoreDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventStoreClient(configuration["EventStore:ConnectionString"]!);
        
        return services;
    }

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAggregateStore<EsdbEventStore>();
        
        var streamNameMap = new StreamNameMap();
        services.AddSingleton(streamNameMap);

        var commandMap = new MessageMap();
        services.AddSingleton(commandMap);

        services
            .AddClientModule(streamNameMap)
            .AddOrganizationModule(streamNameMap);
        
        return services;
    }

    private static IServiceCollection AddClientModule(
        this IServiceCollection services, 
        StreamNameMap streamNameMap)
    {
        streamNameMap.Register<ClientId>(clientId => new StreamName($"client-{clientId.Value}"));
        
        services.AddCommandService<ClientCommandService, Client>();
        TypeMap.RegisterKnownEventTypes(typeof(ClientEvents.V1.ClientCreated).Assembly);
        
        return services;
    }
    
    private static IServiceCollection AddOrganizationModule(this IServiceCollection services, StreamNameMap streamNameMap)
    {
        streamNameMap.Register<OrganizationId>(organizationId => 
            new StreamName($"organization-{organizationId.TenantId}-{organizationId.Value}"));
        services.AddCommandService<OrganizationCommandService, Organization>();
        TypeMap.RegisterKnownEventTypes(typeof(OrganizationEvents.V1.OrganizationCreated).Assembly);

        return services;
    }

    private static void AddTelemetry(this IServiceCollection services)
    {
        services.AddEventuousSpyglass();
    }
}