using System;
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
    public class VersionController : Controller
    {
        // GET: Version
        VersioModal vm = new VersioModal();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VersionList()
        {

            return View(new VersionDataRepository().GetAllVersio());
        }

        public ActionResult VesionDetails(int id)
        {


            return View(new VersionDataRepository().GetVersioByID(id));

        }

        public ActionResult VersionUpdate(int id)
        {


            return View(new VersionDataRepository().GetVersioByID(id));

        }


        [HttpPost]
        public ActionResult VersionUpdate(VersioModal obj1)
        {
            if (ModelState.IsValid)
            {

                vm.StartVesion = obj1.StartVesion;
                vm.EndVersion = obj1.EndVersion;
                vm.Id = obj1.Id;

                new VersionDataRepository().UpdateVersio(vm);
                return RedirectToAction("VersionList");
            }

            return View();
        }

        public ActionResult DeleteVersion(int id)
        {
            new VersionDataRepository().DeleteVersio(id);
            return RedirectToAction("VersionList");
        }

        public ActionResult AddVersion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVersion(VersioModal obj)
        {
            if (ModelState.IsValid)
            {
                vm.Id = obj.Id;
                vm.StartVesion = obj.StartVesion;
                vm.EndVersion = obj.EndVersion;

                if (vm.Id == 0)
                {
                    new VersionDataRepository().InsertVersio(vm);
                }
                ViewData.Model = vm;
                return RedirectToAction("VersionList");
            }

            return View();
        }



    }
}