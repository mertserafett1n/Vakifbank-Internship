namespace LoanPlan.Models.Domain
{
    public class Log
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string LogLevel { get; set; } = "INFO";
        public string Message { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public string? StackTrace { get; set; }
    }
}