using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class CoursesController : Controller
{
    private readonly SchoolContext _context;

    public CoursesController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.ToListAsync();
        return View(courses);
    }

    public IActionResult Create()
    {
        return View("CreateEdit", new Course());
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        return View("CreateEdit", course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Course course)
    {
        if (!ModelState.IsValid)
        {
            return View("CreateEdit", course);
        }

        if (course.CourseID == 0)
        {
            _context.Add(course);
        }
        else
        {
            try
            {
                _context.Update(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Courses.Any(e => e.CourseID == course.CourseID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var course = await _context.Courses
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(m => m.CourseID == id);

        if (course == null) return NotFound();

        ViewData["IsDeleteView"] = false;
        return View("DetailsDelete", course);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var course = await _context.Courses
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(m => m.CourseID == id);

        if (course == null) return NotFound();

        ViewData["IsDeleteView"] = true;
        return View("DetailsDelete", course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Clone(int id)
    {
        var course = _context.Courses
            .FirstOrDefault(m => m.CourseID == id);

        if (course == null)
        {
            return NotFound();
        }

        var clonedCourse = new Course
        {
            Title = course.Title,
            Credits = course.Credits,
        };

        _context.Add(clonedCourse);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
