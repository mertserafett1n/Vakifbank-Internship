using LoanPlan.Data;
using LoanPlan.Models.Domain;

namespace LoanPlan.Services
{
    public class LogService
    {
        private readonly LoanPlanDbContext _dbContext;

        public LogService(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogInfo(string message, string? userEmail = null)
        {
            SaveLog("INFO", message, userEmail, null);
        }

        public void LogError(string message, string? stackTrace, string? userEmail = null)
        {
            SaveLog("ERROR", message, userEmail, stackTrace);
        }

        private void SaveLog(string level, string message, string? userEmail, string? stackTrace)
        {
            var log = new Log
            {
                LogLevel = level,
                Message = message,
                UserEmail = userEmail,
                StackTrace = stackTrace
            };
            _dbContext.Logs.Add(log);
            _dbContext.SaveChanges();
        }
    }
}