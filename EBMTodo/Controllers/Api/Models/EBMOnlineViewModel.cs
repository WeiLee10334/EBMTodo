using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMOnlineViewModel
    {
        public string POID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyDepartment { set; get; }

        public string ApplyName { set; get; }

        public string Description { set; get; }

        public DateTime? HandleDateTime { set; get; }

        public string HandleName { set; get; }

        public DateTime? ResolveDateTime { set; get; }

        public string ResponseName { set; get; }

        public string Memo { set; get; }

        public string title { set; get; }

        public int CompleteRate { set; get; }

        public OnlineCategories OnlineCategories { set; get; }
    }
}