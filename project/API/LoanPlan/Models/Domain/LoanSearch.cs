namespace LoanPlan.Models.Domain
{
    public class LoanSearch{
        public Guid Id {get; set;}

        public required int LoanAmount {get; set;}
        public required int LoanPeriod {get; set;}
        public required int Popularity {get; set;}
    }


}