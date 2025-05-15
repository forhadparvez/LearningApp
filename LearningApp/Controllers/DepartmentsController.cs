using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
