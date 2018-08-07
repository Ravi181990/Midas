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
    public class SearchController : Controller
    {
        // GET: Search
        SearchModel sm = new SearchModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchData()
        {


            return View();
        }

        public ActionResult Search_Criteria()
        {
            SearchModel data = new SearchModel()
            {
                CategoryList = new SelectList(new CategoryDataRepository().GetAllCategoryname(), "Id", "CategoryNm"),
                PlantList = new SelectList(new PlantDataRepository().GetAllPlantName(), "Id", "Plant_Nm"),
                TransactionsList = new SelectList(new TCodeDataRepository().GetAllTcodename(), "Id", "T_CodeName"),
                FieldsList = new SelectList(new AvailableFieldDataRepository().GetAllAvailableFieldModelName(), "Id", "AvailableField_Nm"),


            };

            return View(data);
        }


        [HttpPost]
        public ActionResult Search_Criteria(SearchModel obj)
        {
            if (ModelState.IsValid)
            { 
                sm.Name = obj.Name;
                sm.Description = obj.Description;
                sm.Remaks = obj.Remaks;
                sm.Tech_Name = obj.Tech_Name;
                sm.Category = obj.Category;
                sm.Plant = obj.Plant;
                sm.Transactions = obj.Transactions;
                sm.Fields = obj.Fields;

                new SearchDataRepository().InsertSearchData(sm);
                return RedirectToAction("SearchData");
            }

            return View();
        }



    }
}
