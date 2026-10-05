namespace T2LifestyleChecker.Web.Services.Interfaces
{
    public interface IScoringService
    {
        int CalculateScore(
            int age,
            bool drinksMoreThanTwoDays,
            bool smokes,
            bool exercisesMoreThanOneHour);
    }
}
