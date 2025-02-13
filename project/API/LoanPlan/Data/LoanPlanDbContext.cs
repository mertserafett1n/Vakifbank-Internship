using LoanPlan.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanPlan.Data
{
    public class LoanPlanDbContext: DbContext
    {
        public LoanPlanDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Loan> Loans {get; set;}
        public DbSet<Bank> Banks {get; set;}
        public DbSet<Calculation> Calculations {get; set;}
        public DbSet<LoanSearch> LoanSearches {get; set;}
    }
}