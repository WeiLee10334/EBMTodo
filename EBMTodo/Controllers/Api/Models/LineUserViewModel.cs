using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class LineUserViewModel
    {
        public string UID { get; set; }

        public DateTime CreateDateTime { set; get; }

        public string Name { get; set; }
    }
}