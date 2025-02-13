using LoanPlan.Data;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LoanPlan.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LoanPlanDbContext _dbContext;

        public LogsController(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllLogs()
        {
            var logs = _dbContext.Logs.OrderByDescending(l => l.Timestamp).ToList();
            return Ok(logs);
        }
    }
}