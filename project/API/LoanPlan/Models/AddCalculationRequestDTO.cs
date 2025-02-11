namespace LoanPlan.Models
{
    public class AddCalculationRequestDTO
    {
        public required int LoanAmount {get; set;}
        public required int LoanPeriod {get; set;}
        public required decimal InterestRate {get; set;}
    }
}