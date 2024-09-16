using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
