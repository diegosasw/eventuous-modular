using Eventuous;

namespace OrganizationModule;

public record OrganizationId 
    : AggregateId
{
    public string TenantId { get; }
    public OrganizationId(string id, string tenantId) 
        : base(id)
    {
        TenantId = tenantId;
    }
}