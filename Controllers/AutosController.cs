using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using guia_2.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;

namespace guia_2.Controllers
{


    public class AutosController : Controller
    {
        public List<Autos> lstAutos;
        public AutosController()
        {
            var myJsonString = System.IO.File.ReadAllText("Models/Autos.json");
            lstAutos = JsonConvert.DeserializeObject<List<Autos>>(myJsonString);
        }

        public IActionResult Index()
        {
            return View(lstAutos);
        }

        public IActionResult Find(string nombre)
        {
            List<Autos> lstautosfound = new List<Autos>();

            foreach (Autos obj in lstAutos)
            {
                if (obj.Name.ToLower().Contains(nombre.ToLower()))
                {
                    lstautosfound.Add(obj);
                }
            }

            return View("Index", lstautosfound);

        }
        [Authorize(Roles = "ADMIN,DETAIL")]
        public async Task<IActionResult> Details(int Id)
        {

            foreach (var item in lstAutos)
            {
                if (item.Id == Id)
                {
                    return View(item);
                }
            }

            return View(new Autos());
        }

    }
}