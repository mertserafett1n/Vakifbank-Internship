namespace LoanPlan.Models
{
    public class AddLoanRequestDTO{
        public required string Email {get; set;}

        public required string NameSurname {get; set;}

        public required string BankName {get; set;}

        public required string LoanType {get; set;}

        public required int LoanAmount {get; set;}

        public required int LoanPeriod {get; set;}
    }
}