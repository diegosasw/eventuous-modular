namespace ClientModule;

public static class ClientCommands
{
    public record CreateClient(
        ClientId ClientId,
        string DisplayName,
        string AdminEmail);
}