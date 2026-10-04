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

            return Content(
                $"Patient found: {patient.Name}\n" +
                $"NHS Number: {patient.NHSNumber}\n" +
                $"DOB: {patient.Born}"
            );

        }
    }
}
