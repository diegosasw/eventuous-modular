using Eventuous;
using static OrganizationModule.OrganizationCommands;
// ReSharper disable ClassNeverInstantiated.Global

namespace OrganizationModule;

public class OrganizationCommandService
    : CommandService<Organization, OrganizationState, OrganizationId>
{
    public OrganizationCommandService(
        IAggregateStore store,
        StreamNameMap streamNameMap)
        : base(store, streamNameMap: streamNameMap)
    {
        OnNew<CreateOrganization>(
            cmd => new OrganizationId(cmd.Id, cmd.TenantId),
            (organization, cmd) => 
                organization.CreateOrganization(cmd.Id, cmd.TenantId, cmd.DisplayName, cmd.AdminEmail));
    }
}