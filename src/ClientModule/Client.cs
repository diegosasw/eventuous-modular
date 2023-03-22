using Eventuous;
using System.Text.RegularExpressions;
using static ClientModule.ClientExceptions;
using static ClientModule.DomainEvents.ClientEvents;

namespace ClientModule;

public class Client
    : Aggregate<ClientState>
{
    public void CreateClient(string id, string displayName, string adminEmail)
    {
        EnsureDoesntExist();
        static bool IsValid(string input) => Regex.IsMatch(input, @"^[a-z0-9]+$");
        if (!IsValid(id))
        {
            throw new InvalidClientIdException(id);
        }
        var clientCreated = new V1.ClientCreated(id, displayName, adminEmail);
        Apply(clientCreated);
    }
}