using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using T2LifestyleChecker.Web.Services.Interfaces;
using T2LifestyleChecker.Web.ViewModels;

namespace T2LifestyleChecker.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PatientDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patient = await _patientService.GetPatientAsync(model.NHSNumber);

            if (patient == null)
            {
                return Content("Your details could not be found");
            }

            var nhsNumberMatch = model.NHSNumber == patient.NHSNumber;
            var surnameMatch = model.Name.ToLower() == patient.Name.Split(',', StringSplitOptions.TrimEntries)[0].ToLower();
            if (!DateTime.TryParseExact(
                patient.Born,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var apiDateOfBirth))
            {
                return Content(
                    "There was a problem reading the patient date of birth.");
            }
            var birthDatesMatch = model.Born == apiDateOfBirth;


            if (!nhsNumberMatch || !surnameMatch || !birthDatesMatch)
            {
                return Content("Your details could not be found");
            }

            var today = DateTime.Today;

            var age = today.Year - apiDateOfBirth.Year;

            if (apiDateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 16)
            {
                return Content(
                    "You are not eligible for this service");
            }

            HttpContext.Session.SetInt32("PatientAge", age);

            return RedirectToAction("Index", "Questionnaire");

        }
    }
}
