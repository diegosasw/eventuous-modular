using Eventuous;

namespace ClientModule;

public record ClientId 
    : AggregateId
{
    public ClientId(string id) 
        : base(id)
    {
    }
}