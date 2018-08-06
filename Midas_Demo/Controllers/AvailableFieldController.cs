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
    public class AvailableFieldController : Controller
    {
        // GET: Midas
        AvailableFieldModel cat = new AvailableFieldModel();
        [HttpGet]
        public ActionResult AvailableFieldList()
        {



            return View(new AvailableFieldDataRepository().GetAllAvailableField());
        }



        public ActionResult AvailableFieldDetails(int id)
        {


            return View(new AvailableFieldDataRepository().GetAvailableFieldByID(id));

        }
        [HttpPost]
        public ActionResult AvailableFieldUpdate(AvailableFieldModel obj1)
        {
            if (ModelState.IsValid)
            {

                cat.AvailableField_Nm = obj1.AvailableField_Nm;
                cat.AvailableField_Status = obj1.AvailableField_Status;
                cat.Id = obj1.Id;

                new AvailableFieldDataRepository().UpdateAvailableField(cat);
                return RedirectToAction("AvailableFieldList");
            }

            return View();
        }


        public ActionResult DeleteAvailableField(int id)
        {
            new AvailableFieldDataRepository().DeleteAvailableField(id);
            return RedirectToAction("AvailableFieldList");
        }


        public ActionResult AvailableFieldName()
        {

            return View(new AvailableFieldDataRepository().GetAllAvailableFieldModelName());
        }

        public ActionResult AddAvailableField()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAvailableField(AvailableFieldModel obj)
        {
            if (ModelState.IsValid)
            {
                cat.Id = obj.Id;
                cat.AvailableField_Nm = obj.AvailableField_Nm;
                cat.AvailableField_Status = obj.AvailableField_Status;

                if (cat.Id == 0)
                {
                    new AvailableFieldDataRepository().InsertAvailableField(cat);
                }
                ViewData.Model = cat;
                return RedirectToAction("AvailableFieldList");
            }

            return View();
        }

    }
}