using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using platzi_asp_net_core.Models;

namespace platzi_asp_net_core.Controllers
{
    public class AlumnoController: Controller
    {
       public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                        var alumno = from alumn in _context.Alumnos
                                        where alumn.Id == id
                                        select alumn;

                        return View(alumno.SingleOrDefault());
            }
            else
            {
               return View("MultiAlumno", _context.Alumnos); 
            }
        }

        public IActionResult MultiAlumno()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAlumno", _context.Alumnos);
        }

         public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.Now;
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            ViewBag.Fecha = DateTime.Now;
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id");
            var curso = _context.Cursos.FirstOrDefault();
            alumno.CursoId = curso.Id;
            _context.Alumnos.Add(alumno);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
           
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", alumno.CursoId);
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CursoId,Id,Nombre")] Alumno alumno)
        {
           ViewBag.Fecha = DateTime.Now;
             var curso = _context.Cursos.FirstOrDefault();
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id",alumno.CursoId);
            alumno.CursoId = curso.Id;
            _context.Alumnos.Update(alumno);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


 public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }
        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       

        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
           _context = context; 
        }
        
    }
}