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

namespace guia_2.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ImagesAutosController : Controller
    {
        
        private readonly AppDbContext Context;
        public ImagesAutosController(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        public async Task<IActionResult> Save(int AutosId, string Url)
        {
            ImagesAutos imga = new ImagesAutos();
            imga.Url = Url;
            imga.Autos = await Context.Autos.FindAsync(AutosId);
            // TODO: Your code here
            /*if (ModelState.IsValid){
                
            }*/
            try
            {
                Context.ImagesAutos.Add(imga);
                await Context.SaveChangesAsync();
                return Redirect("/Auto/Details/" + AutosId);
            }
            catch (Exception e)
            {
                return NotFound(e.ToString() + "\n\n\tError: " + e.Message + "\n\n\tidtipo: " + AutosId);
            }


            //return Redirect("/Tiposdeautos/Index");
        }


        public async Task<IActionResult> Edit(int ImagenId, string Url, int AutosId)
        {
            if (await Context.Imagesauto.FindAsync(ImagenId) != null)
            {
                ImagesAutos imga = await Context.ImagesAutos.FindAsync(ImagenId);

                imga.Url = Url;

                try
                {
                    Context.Update(imga);
                    await Context.SaveChangesAsync();
                    return Redirect("/Auto/Details/" + imga.AutosId);
                }
                catch (Exception e)
                {
                    return NotFound(e.ToString() + "\n\n\tError: " + e.Message + "\n\n\tidtipo: " + AutosId);
                }
            }
            else
                return NotFound(ImagenId);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, int AutosId)
        {
            if (Context.ImagesAutos == null)
            {
                return Problem("Entity set 'AppDbContext.Autos'  is null.");
            }
            var tdeau = await Context.ImagesAutos.FindAsync(Id);
            if (tdeau != null)
            {
                Context.ImagesAutos.Remove(tdeau);
            }

            await Context.SaveChangesAsync();
            return Redirect("/Auto/Details/" + AutosId);
        }

    }
}