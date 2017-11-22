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

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WokingHour { set; get; }

        public string workingType { set; get; }

        public DateTime RecordDateTime { set; get; }

        public string LineUID { set; get; }

        public string WorkerName { set; get; }

        public string ProjectName { set; get; }
    }
}