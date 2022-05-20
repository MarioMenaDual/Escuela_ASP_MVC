using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using platzi_asp_net_core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace platzi_asp_net_core.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                        var curso = from cur in _context.Cursos
                                        where cur.Id == id
                                        select cur;

                        return View(curso.SingleOrDefault());
            }
            else
            {
               return View("MultiCurso", _context.Cursos); 
            }
        }

        public IActionResult MultiCurso()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiCurso", _context.Cursos);
        }
        
        public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.Now;
ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            var escuela = _context.Escuelas.FirstOrDefault();
            curso.EscuelaId = escuela.Id;
            ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "Id", "Nombre");
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return View();
        }
public async Task<IActionResult> Edit(string id)
        {
           var curso = await _context.Cursos.FindAsync(id);
            ViewData["EscuelaId"] = new SelectList(_context.Cursos, "Id", "Nombre", curso.EscuelaId);
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Jornada,Direccion,EscuelaId,Id")] Curso curso)
        {
             ViewBag.Fecha = DateTime.Now;
            var escuela = _context.Escuelas.FirstOrDefault();
            ViewData["EscuelaId"] = new SelectList(_context.Cursos, "Id", "Nombre", curso.EscuelaId);
            curso.EscuelaId = escuela.Id;
            _context.Cursos.Update(curso);
            _context.SaveChanges();

            return View();
        }

         public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(string id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }

        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
           _context = context; 
        }
    }
}