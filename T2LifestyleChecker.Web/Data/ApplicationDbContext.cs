using Microsoft.EntityFrameworkCore;
using T2LifestyleChecker.Web.Entities;

namespace T2LifestyleChecker.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScoringRule> ScoringRules { get; set; }
    }
}
