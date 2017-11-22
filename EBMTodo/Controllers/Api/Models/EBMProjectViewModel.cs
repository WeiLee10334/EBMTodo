using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMProjectViewModel
    {
        public Guid PID { set; get; }

        public string ProjectName { set; get; }

        public DateTime CreateDateTime { set; get; }

        public string ProjectNo { set; get; }
    }
}