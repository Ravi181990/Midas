using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Midas_Demo.Models;
using Midas_Demo.DataRepository;

namespace Midas_Demo.Controllers
{
    public class PlantController : Controller
    {

        Plant pt = new Plant();
        // GET: Plant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plantlist()
        {

            return View(new PlantDataRepository().GetAllPlant());
        }


        public ActionResult PlantDetails(int id)
        {


            return View(new PlantDataRepository().GetCategoryByID(id));
        }

        [HttpPost]
        public ActionResult PlantUpdate(Plant obj1)
        {
            if (ModelState.IsValid)
            {

                pt.Plant_Nm = obj1.Plant_Nm;
                pt.Plant_Status = obj1.Plant_Status;
                pt.Id = obj1.Id;

                new PlantDataRepository().UpdatePlant(pt);
                return RedirectToAction("Plantlist");
            }

            return View();
        }

        public ActionResult AddPlant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPlant(Plant obj)
        {
            if (ModelState.IsValid)
            {
                pt.Id = obj.Id;
                pt.Plant_Nm = obj.Plant_Nm;
                pt.Plant_Status = obj.Plant_Status;

                if (pt.Id == 0)
                {

                    new PlantDataRepository().InsertPlant(pt);
                }

                ViewData.Model = pt;

                return RedirectToAction("Plantlist");
            
            }
            return View();
            
        }


        public ActionResult PlantDelete(int id)
        {

            new PlantDataRepository().DeletePlant(id);

            return RedirectToAction("Plantlist");
        }
    }
}