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
    public class FunctionAreaModel
    {

     public int Id { set; get; }

        public string FunctionArea_Name { set; get; }

        public string FunctionArea_Staus { set; get; }
    }
}