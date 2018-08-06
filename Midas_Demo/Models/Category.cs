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
    public class Category
    {
        public int Id { set; get; }

        public string CategoryNm { set; get; }

        public string Status { set; get; }

    }




     
}