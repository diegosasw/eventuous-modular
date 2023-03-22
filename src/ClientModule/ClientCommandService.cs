using Eventuous;
using static ClientModule.ClientCommands;

// ReSharper disable ClassNeverInstantiated.Global

namespace ClientModule;

public class ClientCommandService
    : CommandService<Client, ClientState, ClientId>
{
    public ClientCommandService(
        IAggregateStore store, 
        StreamNameMap streamNameMap)
        : base(store, streamNameMap:streamNameMap)
    {
        OnNew<CreateClient>(
            cmd => cmd.ClientId,
            (client, cmd) => client.CreateClient(cmd.ClientId, cmd.DisplayName, cmd.AdminEmail));
    }
}