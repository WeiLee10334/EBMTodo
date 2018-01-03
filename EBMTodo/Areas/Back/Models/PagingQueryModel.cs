using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Areas.Back.Models
{
    public class PagingQueryModel
    {
        public PagingQueryModel()
        {
            this.Reverse = true;
            this.Skip = 0;
            this.Length = 0;
            this.OrderBy = "";
            this.Filters = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Filters { set; get; }

        public string OrderBy { set; get; }

        public Boolean Reverse { set; get; }

        public int Skip { set; get; }

        public int Length { set; get; }
    }
}