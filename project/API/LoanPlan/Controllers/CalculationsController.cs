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
// 
                  
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult getPaymentPlan(Guid id)
        {   // BURAYA BAK
            var requestCalculation = _dbContext.Calculations.FirstOrDefault(c => c.Id == id);
            
            if(requestCalculation == null){
                return NotFound("Calculation not found(ID)");
            }
            AddCalculationRequestDTO newRequestCalculation = new AddCalculationRequestDTO{
                LoanAmount = requestCalculation.LoanAmount,
                LoanPeriod = requestCalculation.LoanPeriod,
                InterestRate = requestCalculation.InterestRate
            };
            var paymentPlan = calculatePaymentPlan(newRequestCalculation);
            
            // Might have some problem here with returning the list, could've been inside return func, if it won't work, try it, check it.
              //  var paymentPlan = calculatePaymentPlan(requestCalculation).ToList();
                //var calculations = _dbContext.Calculations.ToList();
                return Ok(paymentPlan);
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
            return Ok(domainModelCalculation.Id);
        }

        // Continue here!!!!!
        private List<PaymentPlan> calculatePaymentPlan(AddCalculationRequestDTO requestCalculation){
            var monthlyInterestRate = (double)requestCalculation.InterestRate / 100; // Convert to double
            var monthlyPayment = (double)requestCalculation.LoanAmount * ((monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, (double)requestCalculation.LoanPeriod)) / ((Math.Pow(1 + monthlyInterestRate, (double)requestCalculation.LoanPeriod)) - 1));

            var paymentPlan = new List<PaymentPlan>();
        
            for(int i = 1; i <= requestCalculation.LoanPeriod; i++){
                // Also here not worknig well, need to fix smth.
                var payment = new PaymentPlan{
                    PaymentNo = i,
                    PaymentAmount = monthlyPayment,
                    AmountOfMoney = monthlyPayment * i, // ÖDENEN
                    RestOfMoney = RestOfMoney - (monthlyPayment * i) // KALAN
                    AmountOfInterest = RestOfMoney * monthlyInterestRate,
                };
                paymentPlan.Add(payment);  
            }
            
            return paymentPlan;
        }
    }
}