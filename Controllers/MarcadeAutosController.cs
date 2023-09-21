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

public class MarcadeAutosController : Controller
{
    private readonly AppDbContext _dbContext;
    public MarcadeAutosController(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IActionResult> Index()
    {
        List<MarcasdeAutos> marcas = await _dbContext.MarcasdeAutos.ToListAsync();
        return View(marcas);
    }
    [Authorize(Roles = "ADMIN,CREATE")]
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Logo,Created,Origin,Dercription")] MarcasdeAutos marcasdeAutos)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Add(marcasdeAutos);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(marcasdeAutos);
    }

[Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Edit(int? Id)
    {
        if (Id == null || _dbContext.MarcasdeAutos == null)
        {
            return NotFound();
        }

        var marcdaut = await _dbContext.MarcasdeAutos.FindAsync(Id);
        if (marcdaut == null)
        {
            return NotFound();
        }

        return View(marcdaut);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,Name,Logo,Created,Origin,Dercription")] MarcasdeAutos marcdaut)
    {
        if (Id != marcdaut.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Update(marcdaut);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcdeAutosExists(marcdaut.Id))
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
        return View(marcdaut);
    }

[Authorize(Roles = "ADMIN,DETAIL")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _dbContext.MarcasdeAutos == null)
        {
            return NotFound();
        }

        MarcasdeAutos? marcaut = await _dbContext.MarcasdeAutos.FirstOrDefaultAsync(n => n.Id == id);
        if (marcaut == null)
        {
            return NotFound();
        }

        return View(marcaut);

    }
[Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _dbContext.MarcasdeAutos == null)
        {
            return NotFound();
        }

        var marcaut = await _dbContext.MarcasdeAutos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (marcaut == null)
        {
            return NotFound();
        }

        return View(marcaut);
    }

    // POST: Auto/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_dbContext.MarcasdeAutos == null)
        {
            return Problem("Entity set 'AppDbContext.Autos'  is null.");
        }
        var marcaut = await _dbContext.MarcasdeAutos.FindAsync(id);
        if (marcaut != null)
        {
            _dbContext.MarcasdeAutos.Remove(marcaut);
        }

        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MarcdeAutosExists(int id)
    {
        return (_dbContext.MarcasdeAutos?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    public async Task<IActionResult> Search(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            List<MarcasdeAutos> mdea = await _dbContext.MarcasdeAutos.Where(c =>
                (c.Name != null && c.Name.Contains(search))).ToListAsync();
            return View("Index", mdea);
        }


         // Redirige a la vista "Index"
         return RedirectToAction("Index");
    }

}