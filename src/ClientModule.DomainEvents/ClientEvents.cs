using Eventuous;

namespace ClientModule.DomainEvents;

public static class ClientEvents
{
    public static class V1
    {
        [EventType("V1.ClientCreated")]
        public record ClientCreated(
            string Id,
            string DisplayName,
            string AdminEmail);
    }
}