namespace LoanPlan.Models
{
    public class AddLoanSearchRequestDTO
    {
        public required int LoanAmount {get; set;}
        public required int LoanPeriod {get; set;}

        // TBC ???. Could be added loan infos and when user click on any bank name. can show the special offers of this bank. Consider that.
    }
}