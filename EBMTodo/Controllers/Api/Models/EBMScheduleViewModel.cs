using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMScheduleViewModel
    {
        public string PSID { set; get; }

        public DateTime ScheduleDateTime { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WokingHour { set; get; }

        public string scheduleType { set; get; }

        public DateTime FinishDateTime { set; get; }

        public string LineUID { set; get; }

        public string WorkerName { set; get; }

        public string Title { set; get; }

        public bool ProgressingFlag { set; get; }

        public string PID { set; get; }

        public string ProjectName { set; get; }
    }
}