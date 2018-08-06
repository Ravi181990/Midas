using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Midas_Demo.Models;
using Midas_Demo.DataRepository;

namespace Midas_Demo.Controllers
{
    public class TcodeController : Controller
    {

        TcodeModel pt = new TcodeModel();
        // GET: Plant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TCodeList()
        {

            return View(new TCodeDataRepository().GetAllTcode());
        }


        public ActionResult TcodeDetails(int id)
        {


            return View(new TCodeDataRepository().GetTcodeByID(id));
        }

        [HttpPost]
        public ActionResult TcodeUpdate(TcodeModel obj1)
        {
            if (ModelState.IsValid)
            {

                pt.T_CodeName = obj1.T_CodeName;

                pt.Tcode_Status = obj1.Tcode_Status;
                pt.Id = obj1.Id;

                new TCodeDataRepository().UpdateTcode(pt);
                return RedirectToAction("TCodeList");
            }

            return View();
        }

        public ActionResult AddTcode()
        {
            TcodeModel data = new TcodeModel()
            {
                FunctionArealist = new SelectList(new FunctionAreaDataRepository().GetAllfunctionname(), "Id", "FunctionArea_Name"),
               

            };

            return View(data);
        }

        [HttpPost]
        public ActionResult AddTcode(TcodeModel obj)
        {
            if (ModelState.IsValid)
            {
                pt.Id = obj.Id;
                pt.T_CodeName = obj.T_CodeName;
                pt.FunctionArea = obj.FunctionArea;
                pt.Tcode_Status = obj.Tcode_Status;

                if (pt.Id == 0)
                {

                    new TCodeDataRepository().InsertTcode(pt);
                }

                ViewData.Model = pt;

                return RedirectToAction("TCodeList");

            }
            return View();

        }


        public ActionResult DeleteTcode(int id)
        {

            new TCodeDataRepository().DeleteTcode(id);

            return RedirectToAction("TCodeList");
        }
    }
}