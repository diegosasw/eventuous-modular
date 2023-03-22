using Eventuous;
using System.Text.RegularExpressions;
using static OrganizationModule.OrganizationExceptions;
using static OrganizationModule.DomainEvents.OrganizationEvents;

namespace OrganizationModule;

public class Organization
    : Aggregate<OrganizationState>
{
    public void CreateOrganization(string id, string tenantId, string displayName, string adminEmail)
    {
        EnsureDoesntExist();
        static bool IsValid(string input) => Regex.IsMatch(input, @"^[a-z0-9]+$");
        if (!IsValid(id))
        {
            throw new InvalidOrganizationIdException(id);
        }
        var organizationCreated = new V1.OrganizationCreated(id, tenantId, displayName, adminEmail);
        Apply(organizationCreated);
    }
}