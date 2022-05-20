using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using platzi_asp_net_core.Models;

namespace platzi_asp_net_core.Controllers
{
    public class AsignaturaController: Controller
    {
 [Route("Asignatura/Index")]      
[Route("Asignatura/Index/{asignaturaId}")]
        public IActionResult Index(string asignaturaId)
        {
            if (!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
            where asig.Id == asignaturaId
            select asig;
            return View(asignatura.SingleOrDefault());
            }
            else{
                return View("MultiAsignatura",_context.Asignaturas);
            }
            
        }
         public IActionResult MultiAsignatura()
        {
            var listaAsignaturas = _context.Asignaturas;


            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View(listaAsignaturas);
        }
    
         public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursoId,Id,Nombre")] Asignatura asignatura)
        {
                _context.Add(asignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", asignatura.CursoId);
            return View(asignatura);
        }

        public async Task<IActionResult> Edit(string id)
        {
            
            var asignatura = await _context.Asignaturas.FindAsync(id);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", asignatura.CursoId);
            return View(asignatura);
        }

        // POST: Asignatura/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CursoId,Id,Nombre")] Asignatura asignatura)
        {
            
             _context.Update(asignatura);
            await _context.SaveChangesAsync();
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", asignatura.CursoId);
            return View(asignatura);
        }


public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignaturas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }
        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asignatura = await _context.Asignaturas.FindAsync(id);
            _context.Asignaturas.Remove(asignatura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context){
            _context = context;
        }
    }
}