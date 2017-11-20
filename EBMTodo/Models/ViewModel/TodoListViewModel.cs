using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBMTodo.Models.ViewModel
{
    public class TodoListViewModel
    {
        [Display(Name = "開始時間")]
        public DateTime CreateDateTime { get; set; }

        [Display(Name = "完成時間")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "標題")]
        public string Title { get; set; }

        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "完成度")]
        public int CompleteRate { get; set; }
    }
}