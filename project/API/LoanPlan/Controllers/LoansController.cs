using LoanPlan.Data;
using LoanPlan.Models;
using LoanPlan.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanPlan.Controllers
{
    [Route("api/Loans")]
    [ApiController]
    public class LoansController : ControllerBase 
    {

        private readonly LoanPlanDbContext _dbContext;

        public LoansController(LoanPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [Microsoft.AspNetCore.Mvc.HttpGet]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult GetAllLoans()
        {
                var loans = _dbContext.Loans.ToList();
                return Ok(loans);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddLoan (AddLoanRequestDTO requestLoan)
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
            return Ok(domainModelLoan);
        }
        //[Microsoft.AspNetCore.Mvc.HttpDelete]
        //public IActionResult DeleteLoan (Guid id)
        

    }

}