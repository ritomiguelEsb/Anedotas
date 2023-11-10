using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anedotas.Data;
using Anedotas.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Anedotas.Controllers
{
    public class AnedotaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnedotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anedota
        public async Task<IActionResult> Index()
        {
              return _context.AnedotaModel != null ? 
                          View(await _context.AnedotaModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnedotaModel'  is null.");
        }

        public async Task<IActionResult> SearchForm(string titulo)
        {
            return _context.AnedotaModel != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.AnedotaModel'  is null.");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAnedota(string titulo)
        {
            return _context.AnedotaModel != null ?
                        View("Index",await _context.AnedotaModel.Where(x => x.Titulo.Contains(titulo)).ToListAsync() ?? null) :
                        Problem("Entity set 'ApplicationDbContext.AnedotaModel'  is null.");
        }

        // GET: Anedota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnedotaModel == null)
            {
                return NotFound();
            }

            var anedotaModel = await _context.AnedotaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anedotaModel == null)
            {
                return NotFound();
            }

            return View(anedotaModel);
        }

        // GET: Anedota/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anedota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Contexto,ContextoResposta,Autor")] AnedotaModel anedotaModel)
        {
            if (ModelState.IsValid)
            {
                var autor = User.FindFirstValue(ClaimTypes.NameIdentifier);
                anedotaModel.Autor = autor;
                _context.Add(anedotaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anedotaModel);
        }



        // GET: Anedota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnedotaModel == null)
            {
                return NotFound();
            }

            var anedotaModel = await _context.AnedotaModel.FindAsync(id);
            if (anedotaModel == null)
            {
                return NotFound();
            }
            return View(anedotaModel);
        }

        // POST: Anedota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Contexto,ContextoResposta")] AnedotaModel anedotaModel)
        {
            if (id != anedotaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anedotaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnedotaModelExists(anedotaModel.Id))
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
            return View(anedotaModel);
        }

        // GET: Anedota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnedotaModel == null)
            {
                return NotFound();
            }

            var anedotaModel = await _context.AnedotaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anedotaModel == null)
            {
                return NotFound();
            }

            return View(anedotaModel);
        }

        // POST: Anedota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnedotaModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnedotaModel'  is null.");
            }
            var anedotaModel = await _context.AnedotaModel.FindAsync(id);
            if (anedotaModel != null)
            {
                _context.AnedotaModel.Remove(anedotaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnedotaModelExists(int id)
        {
          return (_context.AnedotaModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
