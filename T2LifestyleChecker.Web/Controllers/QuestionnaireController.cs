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

            if (score <= 3)
            {
                return View("~/Views/Shared/Result.cshtml", new ResultViewModel
                {
                    Title = "Thank you",
                    Message = "Thank you for answering our questions, we don't need to see you at this time. Keep up the good work!",
                    Score = score,
                    IsError = false
                });
            }

            return View("~/Views/Shared/Result.cshtml", new ResultViewModel
            {
                Title = "Your results",
                Message = "We think there are some simple things you could do to improve your quality of life, please phone to book an appointment",
                Score = score,
                IsError = false
            });
        }
    }
}