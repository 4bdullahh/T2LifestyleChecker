namespace T2LifestyleChecker.Web.Entities
{
    public class ScoringRule
    {
        public int Id { get; set; }

        public string AgeBand { get; set; } = string.Empty;

        public int Q1Points { get; set; }

        public int Q2Points { get; set; }

        public int Q3Points { get; set; }
    }
}
