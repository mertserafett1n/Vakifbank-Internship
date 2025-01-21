using System.Numerics;

namespace LoanPlan.Models.Domain
{

    public class Loan
    {
        public required string Email {get; set;}

        public required string NameSurname {get; set;}

        public required string BankName {get; set;}

        public required string LoanType {get; set;}

        public required int LoanAmount {get; set;}

        public required int LoanPeriod {get; set;}
        
    }

}