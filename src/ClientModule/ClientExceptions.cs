namespace ClientModule;

public static class ClientExceptions
{
    public class InvalidClientIdException
        : Exception
    {
        public InvalidClientIdException(string id)
            : base($"Invalid client Id {id}")
        {
        }   
    }
}