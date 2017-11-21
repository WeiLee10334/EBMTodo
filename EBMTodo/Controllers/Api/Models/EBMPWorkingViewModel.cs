using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMPWorkingViewModel
    {
        public Guid PWID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WokingHour { set; get; }

        public WorkingType workingType { set; get; }

        public DateTime RecordDateTime { set; get; }

        public string LineUID { set; get; }

    }
}