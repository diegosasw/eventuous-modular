namespace ClientModule;

public static class ClientCommandsHttp
{
    /// <summary>
    /// Creates a client.
    /// </summary>
    /// <param name="Id">the client's unique identifier (i.e: aggregate root Id)</param>
    /// <param name="DisplayName">the client's display name</param>
    /// <param name="AdminEmail">the client administrator's email</param>
    public record CreateClientHttp(
        string Id,
        string DisplayName,
        string AdminEmail);
}