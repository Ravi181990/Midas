using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Midas_Demo.Models
{
    public class TcodeModel
    {
        public int Id { set; get; }

        public String T_CodeName { set; get; }

        public String FunctionArea { set; get; }

        public SelectList FunctionArealist { set; get; }

        public String Tcode_Status { set; get; }

    }
}