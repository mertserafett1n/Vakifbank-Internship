namespace LoanPlan.Models.Domain
{
    public class Calculation
    {
        public Guid Id {get; set;}

        public required int LoanAmount {get; set;}

        public required int LoanPeriod {get; set;}

        public required decimal InterestRate {get; set;}
    }
}