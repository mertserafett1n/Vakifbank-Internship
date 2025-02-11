using LoanPlan.Data;
using LoanPlan.Models;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanPlan.Controllers
{
    [Route("api/Calculations")]
    [ApiController]
    public class CalculationsController : ControllerBase 
    {

        private readonly LoanPlanDbContext _dbContext;

        public CalculationsController(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [Microsoft.AspNetCore.Mvc.HttpGet]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult GetAllCalculations()
        {
                var calculations = _dbContext.Calculations.ToList();
                return Ok(calculations);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddCalculation (AddCalculationRequestDTO requestCalculation)
        {
            var domainModelCalculation = new Calculation
            {
                Id = Guid.NewGuid(),
                LoanAmount = requestCalculation.LoanAmount,
                LoanPeriod = requestCalculation.LoanPeriod,
                InterestRate = requestCalculation.InterestRate
            };
            _dbContext.Calculations.Add(domainModelCalculation);
            _dbContext.SaveChanges();
            return Ok(domainModelCalculation);
        }
    }

}