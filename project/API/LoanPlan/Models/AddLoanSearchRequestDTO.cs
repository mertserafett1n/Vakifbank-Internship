namespace LoanPlan.Models
{
    public class AddLoanSearchRequestDTO
    {
        public required int LoanAmount {get; set;}
        public required int LoanPeriod {get; set;}
    }
}