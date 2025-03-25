using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Grades.Models;
using Grades.Data;
using Microsoft.EntityFrameworkCore;
namespace Grades.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly MySQLDbContext _context;
        public SubjectsController(MySQLDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> Subject(int id)
        {
            Subject_? subject = await _context.subjects.FindAsync(id);
            if (subject == null) return NotFound("Item no encontrado");
            return View("Item Muestra", subject);
        }
        [HttpGet]
        public async Task<IActionResult> Subjects(int id)
        {
            List<Subject_> subject = await _context.subjects.ToListAsync();
            return View(subject);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Subject_ subject)
        {
            if (subject == null)
            {
                return Error();
            }
            if (ModelState.IsValid)
            {
                _context.subjects.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subject);
        }
        public async Task<IActionResult> Modificar(int id)
        {
            var subject = await _context.subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, Subject_ subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Content("ocurrio un error al actualizar");
                }
                return RedirectToAction("Subjects");
            }
            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Subject_? subject = await _context.subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmedDelete(int id)
        {
            var subject = await _context.subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _context.subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Subjects");
        }
    }
}
