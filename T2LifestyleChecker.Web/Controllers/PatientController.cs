using Microsoft.AspNetCore.Mvc;
using T2LifestyleChecker.Web.ViewModels;

namespace T2LifestyleChecker.Web.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(PatientDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Content(
                    $"NHS Number: {model.NHSNumber}\n" +
                    $"Surname: {model.Name}\n" +
                    $"Date of Birth: {model.Born:dd/MM/yyyy}"
                );
            }
            return View(model);
        }
    }
}
