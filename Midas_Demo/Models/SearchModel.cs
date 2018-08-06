using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Midas_Demo.Models
{
    public class SearchModel
    {

        public int Id { set; get; }

        
        public string Name { set; get; }

        public string Description { set; get; }

        public string Remaks { set; get; }

        public string Tech_Name { set; get; }

        public string Category { set; get; }

        public SelectList CategoryList { set; get; }

        public string Plant { set; get; }

         public SelectList PlantList { set; get; }

        public string Transactions { set; get; }

        public SelectList TransactionsList { set; get; }

        public string Fields { set; get; }

        public SelectList FieldsList { set; get; }

        public float DashboardVersion { set; get; }
    }
}