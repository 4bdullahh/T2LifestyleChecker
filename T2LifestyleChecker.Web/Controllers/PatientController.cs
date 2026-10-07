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

        // Responsible for handling the form submission and validating the patient details
        [HttpPost]
        public async Task<IActionResult> Index(PatientDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Call the patient service to get the patient details based on the provided nhs number
            var patient = await _patientService.GetPatientAsync(model.NHSNumber);

            if (patient == null)
            {
                return View("~/Views/Shared/Result.cshtml", new ResultViewModel
                {
                    Title = "Details not found",
                    Message = "Your details could not be found",
                    IsError = true
                });
            }

            // Compare the details from the API with the user input
            var nhsNumberMatch = model.NHSNumber == patient.NHSNumber;
            var surnameMatch = model.Name.ToLower() == patient.Name.Split(',', StringSplitOptions.TrimEntries)[0].ToLower();
            if (!DateTime.TryParseExact(
                patient.Born,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var apiDateOfBirth))
            {
                return View("~/Views/Shared/Result.cshtml", new ResultViewModel
                {
                    Title = "Error",
                    Message = "There was a problem reading the patient date of birth.",
                    IsError = true
                });
            }
            var birthDatesMatch = model.Born == apiDateOfBirth;

            
            if (!nhsNumberMatch || !surnameMatch || !birthDatesMatch)
            {
                return View("~/Views/Shared/Result.cshtml", new ResultViewModel
                {
                    Title = "Details not found",
                    Message = "Your details could not be found",
                    IsError = true
                });
            }

            var today = DateTime.Today;

            var age = today.Year - apiDateOfBirth.Year;

            if (apiDateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 16)
            {
                return View("~/Views/Shared/Result.cshtml", new ResultViewModel
                {
                    Title = "Not eligible",
                    Message = "You are not eligible for this service",
                    IsError = true
                });
            }

            // Store the patients age in the session for later use
            HttpContext.Session.SetInt32("PatientAge", age);

            return RedirectToAction("Index", "Questionnaire");

        }
    }
}