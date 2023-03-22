namespace OrganizationModule;

public static class OrganizationCommandsHttp
{
    /// <summary>
    /// Creates an organization.
    /// </summary>
    /// <param name="Id">the organization's unique identifier (i.e: aggregate root Id)</param>
    /// <param name="DisplayName">the organization's display name</param>
    /// <param name="AdminEmail">the organization administrator's email</param>
    public record CreateOrganizationHttp(
        string Id,
        string DisplayName,
        string AdminEmail);
}