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
                  
        [Microsoft.AspNetCore.Mvc.HttpGet]  // When it is only HttpGet, didnt work out, respons error(500) ,pretty annoying, stupid error.
        public IActionResult getPaymentPlan()
        {   // BURAYA BAK
            //var requestCalculation = new Calculation{};
            
            // Might have some problem here with returning the list, could've been inside return func, if it won't work, try it, check it.
              //  var paymentPlan = calculatePaymentPlan(requestCalculation).ToList();
                //var calculations = _dbContext.Calculations.ToList();
                return Ok();
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

        // Continue here!!!!!
        private List<PaymentPlan> calculatePaymentPlan(AddCalculationRequestDTO requestCalculation){
            var monthlyInterestRate = (double)requestCalculation.InterestRate / 100; // Convert to double
            var monthlyPayment = (double)requestCalculation.LoanAmount * ((monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, (double)requestCalculation.LoanPeriod)) / ((Math.Pow(1 + monthlyInterestRate, (double)requestCalculation.LoanPeriod)) - 1));

            var paymentPlan = new List<PaymentPlan>();
        
            for(int i = 1; i <= requestCalculation.LoanPeriod; i++){
                var payment = new PaymentPlan{
                    PaymentNo = i,
                    PaymentAmount = monthlyPayment,
                    AmountOfInterest = monthlyPayment * monthlyInterestRate,
                    AmountOfMoney = monthlyPayment * i, // ÖDENEN
                    RestOfMoney = requestCalculation.LoanAmount - (monthlyPayment * i) // KALAN
                };
                paymentPlan.Add(payment);  // This line causes the error, because it returns a list, but it doesn't add each payment plan to it.  // Solution: Move the creation of paymentPlan to the for loop.  // Better solution: Use a database to store the payment plans, not a list.  // But for now, we'll use a list.  // Also, you should return a single object (PaymentPlan) instead of a list when returning the payment
            }
            return paymentPlan;
        }
    }
}