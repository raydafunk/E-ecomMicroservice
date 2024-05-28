namespace Ordering.Domain.Expections;

public class DomainExpection : Exception
{
    public DomainExpection(string message) 
        : base($"Domain Exception:\"{message}\" throws error from Domain layer")
    {     
    }
}