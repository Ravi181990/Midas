using Midas_Demo.DataRepository;
using Midas_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Midas_Demo.Controllers
{
    public class DataEnteryController : Controller
    {
        // GET: DataEntery
        DataEntry datent = new DataEntry();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewData()
        {
            DataEntry data = new DataEntry()
            {
                CategoryList = new SelectList(new CategoryDataRepository().GetAllCategory(), "Id", "CategoryNm"),
                 PlantList = new SelectList(new PlantDataRepository().GetAllPlantName(), "Id", "Plant_Nm"),
                 Frequencylist  = new SelectList(new FrequencyDataRepository().GetAllFrequencyname(),"Id", "Frequency_Nm"),
                 Tcodelist=new SelectList(new TCodeDataRepository().GetAllTcodename(),"Id", "T_CodeName"),
                FunctionalArealist = new SelectList(new FunctionAreaDataRepository().GetAllfunctionname(), "Id", "FunctionArea_Name"),
                AvailableFieldlist = new SelectList(new AvailableFieldDataRepository().GetAllAvailableFieldModelName(), "Id", "AvailableField_Nm"),
                DashboardVersionlist = new SelectList(new VersionDataRepository().GetAllVersioName(), "Id", "Version"),
            };

            return View(data);
        }
        [HttpPost]
        public ActionResult AddNewData(DataEntry entity)
        {
            if (ModelState.IsValid)
            {
                datent.Id = entity.Id;
                datent.Name = entity.Name;
                datent.Description = entity.Description;
                datent.Category = entity.Category;
                datent.TechnicalName = entity.TechnicalName;
                datent.Plants = entity.Plants;
                datent.Frequency = entity.Frequency;
                datent.DashboardVersion = entity.DashboardVersion;
                datent.tcodes = entity.tcodes;
                datent.FunctionalArea = entity.FunctionalArea;
                datent.AvailableFields = entity.AvailableFields;
                datent.DocumentLink = entity.DocumentLink;
                datent.UserGuides = entity.UserGuides;
                datent.Remarks = entity.Remarks;
                if (datent.Id == 0)
                {
                    new DataEntryDataRepository().InsertDataEntry(datent);

                }
                ViewData.Model = datent;
                return RedirectToAction("AddNewData");
            }

            return View();
        }
    }
}