using Midas_Demo.DataRepository;
using Midas_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Midas_Demo.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        SearchModel ser = new SearchModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search_Criteria()
        {
            SearchModel data = new SearchModel()
            {
              CategoryList= new SelectList(new CategoryDataRepository().GetAllCategoryname(), "Id", "CategoryNm"),
              PlantList =new SelectList(new PlantDataRepository().GetAllPlantName(), "Id", "Plant_Nm"),
              TransactionsList=new SelectList(new TCodeDataRepository().GetAllTcodename(), "Id", "T_CodeName"),
              FieldsList=new SelectList(new AvailableFieldDataRepository().GetAllAvailableFieldModelName(), "Id", "AvailableField_Nm"),


            };

            return View(data);
        }

        [HttpPost]

        public ActionResult Search_Criteria(SearchModel obj)
        {
            if (ModelState.IsValid)
            {
                ser.Name = obj.Name;
                ser.Description = obj.Description;
                ser.Remaks = obj.Remaks;
                ser.Category = obj.Category;
                ser.Plant = obj.Plant;
                ser.Transactions = obj.Transactions;
                ser.Fields = obj.Fields;
                new SearchDataRepository().InsertSearchData(ser);
            }

            return View();
        }
    }
}