using LoanPlan.Data;
using LoanPlan.Models;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanPlan.Controllers
{
    [Route("api/Banks")]
    [ApiController]
    public class BanksController : ControllerBase 
    {

        private readonly LoanPlanDbContext _dbContext;

        public BanksController(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [Microsoft.AspNetCore.Mvc.HttpGet]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult GetAllBanks()
        {
                var banks = _dbContext.Banks.ToList();
                return Ok(banks);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddBank (AddBankRequestDTO requestBank)
        {
            var domainModelBank = new Bank
            {
                Id = Guid.NewGuid(),
                Name = requestBank.Name,
                Popularity = requestBank.Popularity
            };
            _dbContext.Banks.Add(domainModelBank);
            _dbContext.SaveChanges();
            return Ok(domainModelBank);
        }

    }

}