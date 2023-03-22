namespace OrganizationModule;

public static class OrganizationExceptions
{
    public class InvalidOrganizationIdException
        : Exception
    {
        public InvalidOrganizationIdException(string id)
            : base($"Invalid organization Id {id}")
        {
        }   
    }
}