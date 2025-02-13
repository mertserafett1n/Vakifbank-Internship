using LoanPlan.Data;
using LoanPlan.Models;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanPlan.Controllers
{
    [Route("api/LoanSearchs")]
    [ApiController]
   public class LoanSearchController : ControllerBase
    {
        private readonly LoanPlanDbContext _dbContext;

        public LoanSearchController(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult getAllSearchLoans()
        {

            var loans = _dbContext.LoanSearches.OrderByDescending(x => x.Popularity).ToList();
            return Ok(loans);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult addSearchLoans(AddLoanSearchRequestDTO request){
            var existingLoanSearches = _dbContext.LoanSearches
                .FirstOrDefault(x => x.LoanAmount == request.LoanAmount 
                                &&
                 x.LoanPeriod == request.LoanPeriod);
                                                
            if(existingLoanSearches != null){
                existingLoanSearches.Popularity += 1;
                _dbContext.SaveChanges();
                return Ok(existingLoanSearches);
            }
            var domainModelLoanSearch = new LoanSearch
            {
                Id = Guid.NewGuid(),
                LoanAmount = request.LoanAmount,
                LoanPeriod = request.LoanPeriod,
                Popularity = 1
            };
            _dbContext.LoanSearches.Add(domainModelLoanSearch);
             _dbContext.SaveChanges();
            return Ok(domainModelLoanSearch);
        }
    }

}