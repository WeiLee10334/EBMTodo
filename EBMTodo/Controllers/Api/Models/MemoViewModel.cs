using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class MemoViewModel
    {
        public int MID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public string Tag { set; get; }

        public string Content { set; get; }

        public string memoType { set; get; }

        public string LineUID { set; get; } 

        public string WorkerName { set; get; }

        public string memo { set; get; }

        public bool ProgressingFlag { set; get; }
    }
}