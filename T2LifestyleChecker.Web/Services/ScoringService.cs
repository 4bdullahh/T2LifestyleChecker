using T2LifestyleChecker.Web.Data;
using T2LifestyleChecker.Web.Services.Interfaces;

namespace T2LifestyleChecker.Web.Services
{
    public class ScoringService : IScoringService
    {
        private readonly ApplicationDbContext _context;

        public ScoringService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CalculateScore(
            int age,
            bool drinksMoreThanTwoDays,
            bool smokes,
            bool exercisesMoreThanOneHour)
        {
            string ageBand;

            switch (age)
            {
                case >= 16 and <= 21:
                    ageBand = "16-21";
                    break;

                case >= 22 and <= 40:
                    ageBand = "22-40";
                    break;

                case >= 41 and <= 65:
                    ageBand = "41-65";
                    break;

                case >= 66:
                    ageBand = "66+";
                    break;

                default:
                    throw new ArgumentException("No scoring rule exists for this age.");
            }

            // Retrieve the scoring rule for the given age band from the database
            var rule = _context.ScoringRules.FirstOrDefault(x => x.AgeBand == ageBand);

            int score = 0;

            // Calculate the score based on the scoring rule and the users answers
            if (drinksMoreThanTwoDays)
            {
                score = score + rule.Q1Points;
            }

            if (smokes)
            {
                score = score + rule.Q2Points;
            }

            if (!exercisesMoreThanOneHour)
            {
                score = score + rule.Q3Points;
            }

            return score;
        }
    }
}