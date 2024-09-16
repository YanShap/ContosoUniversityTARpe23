using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Models
{
    public class AssignedCourseData : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
