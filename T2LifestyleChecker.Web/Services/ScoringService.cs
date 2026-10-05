using T2LifestyleChecker.Web.Services.Interfaces;

namespace T2LifestyleChecker.Web.Services
{
    public class ScoringService : IScoringService
    {
        public int CalculateScore(
            int age,
            bool drinksMoreThanTwoDays,
            bool smokes,
            bool exercisesMoreThanOneHour)
        {
            int q1Points;
            int q2Points;
            int q3Points;

            if (age >= 16 && age <= 21)
            {
                q1Points = 1;
                q2Points = 2;
                q3Points = 1;
            }
            else if (age >= 22 && age <= 40)
            {
                q1Points = 2;
                q2Points = 2;
                q3Points = 3;
            }
            else if (age >= 41 && age <= 65)
            {
                q1Points = 3;
                q2Points = 2;
                q3Points = 2;
            }
            else if (age >= 66)
            {
                q1Points = 3;
                q2Points = 3;
                q3Points = 1;
            }
            else
            {
                throw new ArgumentException(
                    "Patient must be at least 16 years old.");
            }

            int score = 0;

            if (drinksMoreThanTwoDays)
            {
                score += q1Points;
            }
            if (smokes)
            {
                score += q2Points;
            }
            if (!exercisesMoreThanOneHour)
            {
                score += q3Points;
            }

            return score;
        }
    }
}
