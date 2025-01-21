namespace LoanPlan.Models.Domain
{
    public class Bank
    {
        public Guid Id {get; set;}

        public required string Name {get; set;}

        public int Popularity {get; set;}

        // TBC ???. Could be added loan infos and when user click on any bank name. can show the special offers of this bank. Consider that.
    }


}