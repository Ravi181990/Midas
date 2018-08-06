using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Midas_Demo.Models
{
    public class VersioModal
    {
        public int Id { set; get; }
        [Required]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Enter Only Numeric")]
        public string StartVesion { set; get; }

        public string EndVersion { set; get; }

        public string Version { set; get; }
    }
}