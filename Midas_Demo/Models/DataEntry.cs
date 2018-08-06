using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Midas_Demo.Models
{
    public class DataEntry
    {
        public int Id { set; get; }

        public String Name { set; get; }

        public String Description { set; get; }

        public string Category { set; get; }

        public SelectList CategoryList { set; get; }

        public String TechnicalName { set; get; }

        public string Plants { set; get; }

        public SelectList PlantList { set; get; }
        public string Frequency { set; get; }

        public SelectList Frequencylist { set; get; }

        public float DashboardVersion { set; get; }


        public SelectList DashboardVersionlist { set; get; }
        public string tcodes { set; get; }

        public SelectList Tcodelist { set; get; }

        public string FunctionalArea { set; get; }

        public SelectList FunctionalArealist { set; get; }
        public String AvailableFields { set; get; }

        public SelectList AvailableFieldlist { set; get; }

        public String  DocumentLink { set; get; }

        public String UserGuides { set; get; }
        
        public String Remarks { set; get; }
    }
}