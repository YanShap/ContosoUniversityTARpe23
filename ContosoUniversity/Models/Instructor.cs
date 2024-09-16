using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Models
{
    public class Instructor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
