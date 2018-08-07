using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;

namespace Midas_Demo.Models
{
    public class SearchModel
    {
     
        public string Name { set; get; }

        public string Description { set; get; }

        public string Frecuency { set; get; }

        public string Remaks { set; get; }

        public string Tech_Name { set; get; }

        public List<string> Category { set; get; }

        public SelectList CategoryList { set; get; }

        public List<string> Plant { set; get; }

        public SelectList PlantList { set; get; }

        public List<string> Transactions { set; get; }

        public SelectList TransactionsList { set; get; }

        public List<string> Fields { set; get; }

        public SelectList FieldsList { set; get; }

        public float DashboardVersion { set; get; }

    }
}
