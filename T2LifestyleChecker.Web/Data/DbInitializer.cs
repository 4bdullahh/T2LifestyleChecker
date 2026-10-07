using T2LifestyleChecker.Web.Entities;

namespace T2LifestyleChecker.Web.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.ScoringRules.Any())
            {
                return;
            }

            context.ScoringRules.AddRange(
                new ScoringRule
                {
                    AgeBand = "16-21",
                    Q1Points = 1,
                    Q2Points = 2,
                    Q3Points = 1
                },
                new ScoringRule
                {
                    AgeBand = "22-40",
                    Q1Points = 2,
                    Q2Points = 2,
                    Q3Points = 3
                },
                new ScoringRule
                {
                    AgeBand = "41-65",
                    Q1Points = 3,
                    Q2Points = 2,
                    Q3Points = 2
                },
                new ScoringRule
                {
                    AgeBand = "66+",
                    Q1Points = 3,
                    Q2Points = 3,
                    Q3Points = 1
                });

            context.SaveChanges();
        }
    }
}
