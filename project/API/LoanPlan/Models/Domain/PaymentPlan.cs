namespace LoanPlan.Models.Domain
{
    public class PaymentPlan
    {
        public Guid Id {get; set; }
        public int PaymentNo { get; set; }
        public double PaymentAmount { get; set; }
        public double AmountOfInterest { get; set; }
        public double AmountOfMoney { get; set; }
        public double RestOfMoney { get; set; }

    }

}