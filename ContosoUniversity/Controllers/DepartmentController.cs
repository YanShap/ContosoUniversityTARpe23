using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolContext.ToListAsync());
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * FROM Department WHERE DepartmentId = {0}";
            var department = await _context.Departments
                .FromSqlRaw(query, id)
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.InstructorId = new SelectList(_context.Instructors, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.InstructorId = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorId);
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> BaseOn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            ViewBag.InstructorId = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorId);
            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaseOn(int id, string action, Department department)
        {
            if (action == "Make")
            {
                var newDepartment = new Department
                {
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate,
                    StudentId = department.StudentId,
                    Aadress = department.Aadress,
                    InstructorId = department.InstructorId
                };

                _context.Departments.Add(newDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            else if (action == "MakeAndDeleteOld")
            {

                var newDepartment = new Department
                {
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate,
                    StudentId = department.StudentId,
                    Aadress = department.Aadress,
                    InstructorId = department.InstructorId
                };

                _context.Departments.Add(newDepartment);

                var oldDepartment = await _context.Departments.FindAsync(id);
                if (oldDepartment != null)
                {
                    _context.Departments.Remove(oldDepartment);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.InstructorID = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorId);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.InstructorID = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorId);
            return View(department);
        }

    }
}