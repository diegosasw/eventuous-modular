using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using static ClientModule.ClientCommandsHttp;
namespace ClientModule;

public static class ClientCommandsApi
{
    public static WebApplication AddClientCommands(this WebApplication app)
    {
        app
            .MapAggregateCommands<Client>()
            .MapCommand<CreateClientHttp, ClientCommands.CreateClient>(
                "clients/create",
                (cmd, _) =>
                    new ClientCommands.CreateClient(
                        new ClientId(cmd.Id),
                        cmd.DisplayName,
                        cmd.AdminEmail));
                // rhb =>
                //     rhb.RequireAuthorization(new AuthorizeAttribute {Roles = RoleType.DevOps})

                return app;
    }
}