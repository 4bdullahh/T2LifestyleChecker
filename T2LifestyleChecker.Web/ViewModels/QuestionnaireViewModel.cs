using System.ComponentModel.DataAnnotations;

namespace T2LifestyleChecker.Web.ViewModels
{
    public class QuestionnaireViewModel
    {
        [Required(ErrorMessage = "Please answer question 1.")]
        public bool? DrinksMoreThanTwoDays { get; set; }

        [Required(ErrorMessage = "Please answer question 2.")]
        public bool? Smokes { get; set; }

        [Required(ErrorMessage = "Please answer question 3.")]
        public bool? ExercisesMoreThanOneHour { get; set; }
    }
}
