using Microsoft.AspNetCore.Mvc;
using T2LifestyleChecker.Web.ViewModels;

namespace T2LifestyleChecker.Web.Controllers
{
    public class QuestionnaireController : Controller
    {
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

            return Content(
                $"Q1: {model.DrinksMoreThanTwoDays}\n" +
                $"Q2: {model.Smokes}\n" +
                $"Q3: {model.ExercisesMoreThanOneHour}"
            );
        }
    }
}
