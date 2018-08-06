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
    public class MidasController : Controller
    {
        // GET: Midas
        Category cat = new Category();
        [HttpGet]
        public ActionResult CategoryList()
        {

           

            return View(new CategoryDataRepository().GetAllCategory());
        }
        
       

        public ActionResult CategoryDetails(int id)
        {


            return View(new CategoryDataRepository().GetCategoryByID(id));

        }
        [HttpPost]
        public ActionResult CategoryUpdate(Category obj1)
        {
            if (ModelState.IsValid)
            {
               
                cat.CategoryNm = obj1.CategoryNm;
                cat.Status = obj1.Status;
                cat.Id = obj1.Id;

                new CategoryDataRepository().UpdateCategory(cat);
                return RedirectToAction("CategoryList");
            }

            return View();
        }


        public ActionResult DeleteCategory(int id)
        {
            new CategoryDataRepository().DeleteCategory(id);
            return RedirectToAction("CategoryList");
        }
      

        public ActionResult categoryname()
        {

            return View(new CategoryDataRepository().GetAllCategory());
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category obj)
        {
            if (ModelState.IsValid)
            {
                cat.Id = obj.Id;
                cat.CategoryNm = obj.CategoryNm;
                cat.Status = obj.Status;             

                if (cat.Id == 0)
                {
                    new CategoryDataRepository().InsertCategory(cat);
                }
                ViewData.Model = cat;
                return RedirectToAction("CategoryList");
            }

            return View();
        }
    }
}