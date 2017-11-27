using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Globalization;
using LinqKit;

namespace EBMTodo.Controllers.Api
{
    /// <summary>
    /// /api/ebmworking/...
    /// </summary>
    [RoutePrefix("api/ebmpworking")]
    public class EBMPWorkingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [Route("initData")]
        public IHttpActionResult initData()
        {
            var members = db.LineUser.Select(x => new EBMMemberViewModel()
            {
                UID = x.UID,
                Name = x.Name
            }).ToList();
            var projects = db.EBMProject.Select(x => new EBMProjectViewModel()
            {
                PID = x.PID,
                ProjectName = x.ProjectName,
                ProjectNo = x.ProjectNo,
                CreateDateTime = x.CreateDateTime
            }).ToList();
            return Ok(new { members = members, projects = projects });
        }
        [HttpPost]
        [Route("getData")]
        public IHttpActionResult getData(WorkingQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<EBMPWorkingViewModel>(true);
            predicate = predicate.And(x => x.RecordDateTime >= para.start && x.RecordDateTime <= para.end);
          
            if (para.UIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>(false);
                foreach (var id in para.PIDs)
                {
                    orPredicate = orPredicate.Or(x => x.PID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("getDataByTime")]
        public IHttpActionResult getDataByTime(WorkingQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<EBMPWorkingViewModel>(true);
            predicate = predicate.And(x => x.RecordDateTime >= para.start && x.RecordDateTime <= para.end);

            if (para.UIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>(false);
                foreach (var id in para.PIDs)
                {
                    orPredicate = orPredicate.Or(x => x.PID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList()
                .GroupBy(x =>
                {
                    switch (para.groupby)
                    {
                        case "day":
                            return x.RecordDateTime.ToString("yyyy-MM-dd");
                        case "week":
                            return x.RecordDateTime.FirstDayOfWeek().ToString("yyyy-MM-dd") + "~" + x.RecordDateTime.LastDayOfWeek().ToString("yyyy-MM-dd");
                        case "month":
                            DateTime date = x.RecordDateTime;
                            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            return firstDayOfMonth.ToString("yyyy-MM-dd") + "~" + lastDayOfMonth.ToString("yyyy-MM-dd");
                        default:
                            return x.RecordDateTime.ToString("yyyy-MM-dd");
                    }
                })
               .Select(x => new
               {
                   date = x.Key,
                   list = x.ToList()
               }).OrderBy(x => x.date).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("getDataByUID")]
        public IHttpActionResult getDataByUID(WorkingQueryModel para)
        {
            //預設一個禮拜
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<EBMPWorkingViewModel>(true);
            predicate = predicate.And(x => x.RecordDateTime >= para.start && x.RecordDateTime <= para.end);

            if (para.UIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>(false);
                foreach (var id in para.PIDs)
                {
                    orPredicate = orPredicate.Or(x => x.PID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList()
                .GroupBy(x => new
                {
                    UID = x.LineUID,
                    Name = x.WorkerName
                })
               .Select(x => new
               {
                   lineUID = x.Key.UID,
                   workerName = x.Key.Name,
                   list = x.ToList()
               }).ToList();
            return Ok(data);
        }
    }
    public class WorkingQueryModel
    {
        public DateTime? start { set; get; }
        public DateTime? end { set; get; }
        public List<string> UIDs { set; get; }
        public List<string> PIDs { set; get; }
        public string groupby { set; get; }
    }
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            DateTime ldowDate = FirstDayOfWeek(date).AddDays(6);
            return ldowDate;
        }
    }
}
