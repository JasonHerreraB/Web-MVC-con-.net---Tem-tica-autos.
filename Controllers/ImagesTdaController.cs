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
    public class ImagesTdaController : Controller
    {
        private readonly AppDbContext Context;
        public ImagesTdaController(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        public async Task<IActionResult> Save(int TiposdeautosId, string url)
        {
            Imagesauto imga = new Imagesauto();
            imga.url = url;
            imga.Tiposdeauto = await Context.Tiposdeautos.FindAsync(TiposdeautosId);
            // TODO: Your code here
            /*if (ModelState.IsValid){
                
            }*/
            try
            {
                Context.Imagesauto.Add(imga);
                await Context.SaveChangesAsync();
                return Redirect("/Tiposdeautos/Details/" + imga.TiposdeautosId);
            }
            catch (Exception e)
            {
                return NotFound(e.ToString() + "\n\n\tError: " + e.Message + "\n\n\tidtipo: " + TiposdeautosId);
            }


            //return Redirect("/Tiposdeautos/Index");
        }


        public async Task<IActionResult> Edit(int ImagenId, string url, int TiposdeautosId)
        {
            if (await Context.Imagesauto.FindAsync(ImagenId) != null)
            {
                Imagesauto imga = await Context.Imagesauto.FindAsync(ImagenId);

                imga.url = url;

                try
                {
                    Context.Update(imga);
                    await Context.SaveChangesAsync();
                    return Redirect("/Tiposdeautos/Details/" + imga.TiposdeautosId);
                }
                catch (Exception e)
                {
                    return NotFound(e.ToString() + "\n\n\tError: " + e.Message + "\n\n\tidtipo: " + TiposdeautosId);
                }
            }
            else
                return NotFound(ImagenId);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, int TdaId)
        {
            if (Context.Imagesauto == null)
            {
                return Problem("Entity set 'AppDbContext.Autos'  is null.");
            }
            var tdeau = await Context.Imagesauto.FindAsync(Id);
            if (tdeau != null)
            {
                Context.Imagesauto.Remove(tdeau);
            }

            await Context.SaveChangesAsync();
            return Redirect("/Tiposdeautos/Details/" + TdaId);
        }

    }
}