using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Midas_Demo.Models;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;
using Midas_Demo.DataRepository;

namespace Midas_Demo.Controllers
{
    public class FunctionAreaController : Controller
    {
        // GET: Midas
        FunctionAreaModel fua = new FunctionAreaModel();

        public ActionResult FunctionAreaList()
        {



            return View(new FunctionAreaDataRepository().GetAllFunction());
        }



        public ActionResult FunctionAreaDetails(int id)
        {


            return View(new FunctionAreaDataRepository().GetFunctionByID(id));

        }
        [HttpPost]
        public ActionResult FunctionAreaUpdate(FunctionAreaModel obj1)
        {
            if (ModelState.IsValid)
            {

                fua.FunctionArea_Name = obj1.FunctionArea_Name;
                fua.FunctionArea_Staus = obj1.FunctionArea_Staus;
                fua.Id = obj1.Id;

                new FunctionAreaDataRepository().UpdateFunction(fua);
                return RedirectToAction("FunctionAreaList");
            }

            return View();
        }


        public ActionResult DeleteFunction(int id)
        {
            new FunctionAreaDataRepository().DeleteFunction(id);
            return RedirectToAction("FunctionAreaList");
        }


        public ActionResult Functionname()
        {

            return View(new FunctionAreaDataRepository().GetAllfunctionname());
        }

        public ActionResult AddFunction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFunction(FunctionAreaModel obj)
        {
            if (ModelState.IsValid)
            {
                fua.Id = obj.Id;
                fua.FunctionArea_Name = obj.FunctionArea_Name;
                fua.FunctionArea_Staus = obj.FunctionArea_Staus;

                if (fua.Id == 0)
                {
                    new FunctionAreaDataRepository().InsertFunction(fua);
                }
                ViewData.Model = fua;
                return RedirectToAction("FunctionAreaList");
            }

            return View();
        }
    }
}