using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBMTodo.Models.ViewModel
{
    public class StudyViewModel
    {
        public string wlGUID { get; set; }
        public string UID { get; set; }
        [Display(Name = "所屬")]
        public string UName { get; set; }
        [Display(Name = "ScanID")]
        public string ScanID { get; set; }
        [Display(Name = "Macro")]
        public string MacroPath { get; set; }
        [Display(Name = "選用")]
        public bool IsUse { get; set; }
        [Display(Name = "標籤(請用#號分類)")]
        public string Tags { get; set; }
        public DateTime ScanDateTime { get; set; }
        [Display(Name = "描述")]
        public string Description { set; get; }


        [Display(Name = "掃描倍率")]
        public string ScanningMagnification { set; get; }

    }
}