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
    public class FrequencyController : Controller
    {
        // GET: Frequency

        FrequencyModel fm = new FrequencyModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Frequencylist()
        {
            return View(new FrequencyDataRepository().GetAllFrequency());
        }
        public ActionResult FrequencyDetails(int id)
        {


            return View(new FrequencyDataRepository().GetFrequencyByID(id));

        }

        [HttpPost]
        public ActionResult FrequencyUpdate(FrequencyModel obj1)
        {
            if (ModelState.IsValid)
            {

                fm.Frequency_Nm = obj1.Frequency_Nm;
                fm.Status = obj1.Status;
                fm.Id = obj1.Id;

                new FrequencyDataRepository().UpdateFrequency(fm);
                return RedirectToAction("Frequencylist");
            }

            return View();
        }
        public ActionResult DeleteFrequency(int id)
        {
            new FrequencyDataRepository().DeleteFrequency(id);
            return RedirectToAction("Frequencylist");
        }

        public ActionResult Frequencyname()
        {

            return View(new FrequencyDataRepository().GetAllFrequencyname());
        }
        public ActionResult AddFrequency()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFrequency(FrequencyModel obj)
        {
            if (ModelState.IsValid)
            {
                fm.Id = obj.Id;
                fm.Frequency_Nm = obj.Frequency_Nm;
                fm.Status = obj.Status;

                if (fm.Id == 0)
                {
                    new FrequencyDataRepository().InsertFrequency(fm);
                }
                ViewData.Model = fm;
                return RedirectToAction("Frequencylist");
            }


            return View();
        }
    }
}