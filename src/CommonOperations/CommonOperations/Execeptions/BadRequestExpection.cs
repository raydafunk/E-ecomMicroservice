namespace CommonOperations.Execeptions
{
    public class BadRequestExpection : Exception
    {
        public BadRequestExpection(string message) : base(message)
        {
            
        }
        public BadRequestExpection(string message, string details) : base(message)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
