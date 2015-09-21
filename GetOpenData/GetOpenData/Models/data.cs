using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetOpenData.Models
{
    public class data
    {
        [Display(Name = "位置")]
        public string district { get; set; }
        [Display(Name = "地址")]
        public string address { get; set; }
        [Display(Name = "電話")]
        public string tel { get; set; }
        [Display(Name = "時間")]
        public string opening_hours { get; set; }
    }
}