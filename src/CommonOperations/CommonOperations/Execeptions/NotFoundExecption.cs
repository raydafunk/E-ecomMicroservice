namespace CommonOperations.Execeptions;
public class NotFoundExecption : Exception
{
    public NotFoundExecption(string message) : base(message)
    {
            
    }
    public NotFoundExecption(string name, object key) : base($"Entity\"{name}\"({key}) wast not found.")
    {
            
    }
}
