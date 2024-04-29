namespace CommonOperations.Execeptions
{
    public class InternalServerExpection : Exception
    {
        public InternalServerExpection(string message) : base(message)
        {
            
        }
        public InternalServerExpection(string message, string details) : base(message)
        {
            Details = details;
        }
        public string? Details { get; set; }

    }
}
