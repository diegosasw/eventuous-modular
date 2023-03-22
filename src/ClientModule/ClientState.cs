using Eventuous;
using static ClientModule.DomainEvents.ClientEvents;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ClientModule;

public record ClientState
    : State<ClientState>
{
    public string DisplayName { get; init; } = string.Empty;
    public string AdminEmail { get; init; } = string.Empty;

    public ClientState()
    {
        On<V1.ClientCreated>(HandleClientCreated);
    }

    static ClientState HandleClientCreated(ClientState clientState, V1.ClientCreated clientCreated)
        => clientState with
        {
            DisplayName = clientCreated.DisplayName,
            AdminEmail = clientCreated.AdminEmail
        };
}