using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using guia_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace guia_2.Controllers;
public class TiposdeautosController : Controller
{
    private readonly AppDbContext contexto;
    public TiposdeautosController(AppDbContext context)
    {
        contexto = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Tiposdeautos> tdea = await contexto.Tiposdeautos.ToListAsync();
        return View(tdea);
    }
    [Authorize(Roles = "ADMIN,CREATE")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Image,Descripcion")] Tiposdeautos tdea)
    {
        if (ModelState.IsValid)
        {
            contexto.Add(tdea);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tdea);
    }

    [Authorize(Roles = "ADMIN,DETAIL")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || contexto.Tiposdeautos == null)
        {
            return NotFound();
        }

        var tdea = await contexto.Tiposdeautos.Where(c => c.Id == id)
            .Include(c => c.Images).FirstAsync();
        if (tdea == null)
        {
            return NotFound();
        }

        return View(tdea);

    }

[Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Edit(int? Id)
    {
        if (Id == null || contexto.Tiposdeautos == null)
        {
            return NotFound();
        }

        var tdea = await contexto.Tiposdeautos.FindAsync(Id);
        if (tdea == null)
        {
            return NotFound();
        }

        return View(tdea);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,Nombre,Image,Descripcion")] Tiposdeautos tdea)
    {
        if (Id != tdea.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                contexto.Update(tdea);
                await contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(contexto.Tiposdeautos?.Any(e => e.Id == tdea.Id)).GetValueOrDefault())
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
        return View(tdea);
    }

[Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || contexto.Tiposdeautos == null)
        {
            return NotFound();
        }

        var tdea = await contexto.Tiposdeautos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tdea == null)
        {
            return NotFound();
        }

        return View(tdea);
    }

    // POST: Auto/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (contexto.Tiposdeautos == null)
        {
            return Problem("Entity set 'AppDbContext.Autos'  is null.");
        }
        var tdeau = await contexto.Tiposdeautos.FindAsync(id);
        if (tdeau != null)
        {
            contexto.Tiposdeautos.Remove(tdeau);
        }

        await contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Search(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            List<Tiposdeautos> tda = await contexto.Tiposdeautos
                .Where(c => (c.Nombre != null && c.Nombre.Contains(search))).ToListAsync();
            return View("Index", tda);
        }


        // Redirige a la vista "Index"
        return RedirectToAction("Index");
    }

}
