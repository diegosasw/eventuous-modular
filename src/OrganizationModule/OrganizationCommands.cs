namespace OrganizationModule;

public static class OrganizationCommands
{
    /// <summary>
    /// Creates an organization
    /// </summary>
    /// <param name="Id">the organization unique identifier</param>
    /// <param name="TenantId">the tenant Id</param>
    /// <param name="DisplayName">the organization name</param>
    /// <param name="AdminEmail">the organization administrator's email</param>
    public record CreateOrganization(
        string Id,
        string TenantId,
        string DisplayName,
        string AdminEmail);

    /// <summary>
    /// Sets the administrator credentials for the organization
    /// </summary>
    /// <param name="Id">the organization unique identifier</param>
    /// <param name="TenantId">the tenant Id</param>
    /// <param name="Password">the administrator's password</param>
    public record UpdateOrganizationAdminCredentials(
        string Id,
        string TenantId,
        string Password);
}