using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using static OrganizationModule.OrganizationCommandsHttp;
// ReSharper disable StringLiteralTypo

namespace OrganizationModule;

public static class OrganizationCommandsApi
{
    public static WebApplication AddOrganizationCommands(this WebApplication app)
    {
        app
            .MapAggregateCommands<Organization>()
            .MapCommand<CreateOrganizationHttp, OrganizationCommands.CreateOrganization>(
                "organizations/create",
                (cmd, ctx) =>
                {
                    var tenantId = "sasw";
                    var domainCommand =
                        new OrganizationCommands.CreateOrganization(
                            new OrganizationId(cmd.Id, tenantId),
                            tenantId,
                            cmd.DisplayName,
                            cmd.AdminEmail);
                    return domainCommand;
                });
                // rhb =>
                //     rhb.RequireAuthorization(new AuthorizeAttribute {Roles = RoleType.ClientAdmin}))
        
        return app;
    }
}