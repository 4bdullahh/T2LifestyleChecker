using System.ComponentModel.DataAnnotations;

namespace T2LifestyleChecker.Web.ViewModels
{
    public class PatientDetailsViewModel
    {
        [Required(ErrorMessage = "NHS Number is required")]
        public string NHSNumber { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public string Born { get; set; }

    }
}
