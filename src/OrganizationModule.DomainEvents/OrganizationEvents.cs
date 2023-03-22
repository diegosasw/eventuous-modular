using Eventuous;

namespace OrganizationModule.DomainEvents;

public static class OrganizationEvents
{
    public static class V1
    {
        [EventType("V1.OrganizationCreated")]
        public record OrganizationCreated(
            string Id,
            string TenantId,
            string DisplayName,
            string AdminEmail);
    }
}