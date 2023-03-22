using Eventuous;
using static OrganizationModule.DomainEvents.OrganizationEvents;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OrganizationModule;

public record OrganizationState
    : State<OrganizationState>
{
    public string TenantId { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string AdminEmail { get; init; } = string.Empty;

    public OrganizationState()
    {
        On<V1.OrganizationCreated>(HandleClientCreated);
    }

    static OrganizationState HandleClientCreated(OrganizationState organizationState, V1.OrganizationCreated organizationCreated)
        => organizationState with
        {
            TenantId = organizationCreated.TenantId,
            DisplayName = organizationCreated.DisplayName,
            AdminEmail = organizationCreated.AdminEmail
        };
}