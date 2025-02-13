using LoanPlan.Data;
using LoanPlan.Models;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanPlan.Services;
namespace LoanPlan.Controllers
{
    [Route("api/Loans")]
    [ApiController]
    public class LoansController : ControllerBase 
    {

        private readonly LoanPlanDbContext _dbContext;
        private readonly LogService _logService;
        public LoansController(LoanPlanDbContext dbContext, LogService logService)
        {
            _dbContext = dbContext;
            _logService = logService;
        }

        
        [Microsoft.AspNetCore.Mvc.HttpGet]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult GetAllLoans()
        {
                var loans = _dbContext.Loans.ToList();
                _logService.LogInfo("Fetched all loans.");
                return Ok(loans);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddLoan(AddLoanRequestDTO requestLoan)
        {
            var domainModelLoan = new Loan
            {
                Id = Guid.NewGuid(),
                NameSurname = requestLoan.NameSurname,
                BankName = requestLoan.BankName,
                LoanAmount = requestLoan.LoanAmount,
                LoanPeriod = requestLoan.LoanPeriod,
                Email = requestLoan.Email,
                LoanType = requestLoan.LoanType
            };

            _dbContext.Loans.Add(domainModelLoan);
            _dbContext.SaveChanges();

            UpdateLoanSearch(requestLoan.LoanAmount, requestLoan.LoanPeriod);
            _logService.LogInfo($"Loan application created for {requestLoan.NameSurname} ({requestLoan.Email}).");
            return Ok(domainModelLoan);
        }

        private void UpdateLoanSearch(int loanAmount, int loanPeriod)
        {
            var existingLoanSearch = _dbContext.LoanSearches
                .FirstOrDefault(x => x.LoanAmount == loanAmount && x.LoanPeriod == loanPeriod);

            if (existingLoanSearch != null)
            {
                // If already exists, increase popularity
                existingLoanSearch.Popularity += 1;
            }
            else
            {
                // If doesn't exist, create a new entry
                var newLoanSearch = new LoanSearch
                {
                    Id = Guid.NewGuid(),
                    LoanAmount = loanAmount,
                    LoanPeriod = loanPeriod,
                    Popularity = 1 // Start with 1 popularity because it's just created
                };
                _dbContext.LoanSearches.Add(newLoanSearch);
            }
            _dbContext.SaveChanges(); // ✅ Save changes to the database
        }
    }
}