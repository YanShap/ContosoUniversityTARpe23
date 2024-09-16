using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
