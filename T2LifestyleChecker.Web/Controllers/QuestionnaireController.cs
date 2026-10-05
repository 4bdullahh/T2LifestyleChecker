using Microsoft.AspNetCore.Mvc;
using T2LifestyleChecker.Web.Services.Interfaces;
using T2LifestyleChecker.Web.ViewModels;

namespace T2LifestyleChecker.Web.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly IScoringService _scoringService;

        public QuestionnaireController(IScoringService scoringService)
        {
            _scoringService = scoringService;
        }

        public IActionResult Index()
        {
            var patientAge = HttpContext.Session.GetInt32("PatientAge");

            if (patientAge == null)
            {
                return RedirectToAction(
                    "Index",
                    "Patient");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(QuestionnaireViewModel model)
        {
            var patientAge = HttpContext.Session.GetInt32("PatientAge");

            if (patientAge == null)
            {
                return RedirectToAction(
                    "Index",
                    "Patient");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var score = _scoringService.CalculateScore(
                patientAge.Value,
                model.DrinksMoreThanTwoDays!.Value,
                model.Smokes!.Value,
                model.ExercisesMoreThanOneHour!.Value);

            return Content($"Your score is: {score}");
        }
    }
}
