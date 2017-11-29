using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/schedule")]
    public class EBMScheduleController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        [Route("getData")]
        public IHttpActionResult getData(WorkingQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.EBMProjectSchedule.Select(x => new EBMScheduleViewModel
            {
                PSID = x.PSID.ToString(),
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                Target = x.Target,
                WokingHour = x.WokingHour,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name,
                FinishDateTime = x.FinishDateTime,
                scheduleType = x.scheduleType.ToString(),
                ScheduleDateTime = x.ScheduleDateTime,
                ProgressingFlag = x.ProgressingFlag,
                Title = x.Title
            });
            var predicate = PredicateBuilder.New<EBMScheduleViewModel>(true);
            predicate = predicate.And(x => x.ScheduleDateTime >= para.start && x.ScheduleDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null && para.PIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>(false);
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
            var model = db.EBMProjectSchedule.Select(x => new EBMScheduleViewModel
            {
                PSID = x.PSID.ToString(),
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                Target = x.Target,
                WokingHour = x.WokingHour,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name,
                FinishDateTime = x.FinishDateTime,
                scheduleType = x.scheduleType.ToString(),
                ScheduleDateTime = x.ScheduleDateTime,
                ProgressingFlag = x.ProgressingFlag,
                Title = x.Title
            });
            var predicate = PredicateBuilder.New<EBMScheduleViewModel>(true);
            predicate = predicate.And(x => x.ScheduleDateTime >= para.start && x.ScheduleDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null && para.PIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>(false);
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
                            return x.ScheduleDateTime.ToString("yyyy-MM-dd");
                        case "week":
                            return x.ScheduleDateTime.FirstDayOfWeek().ToString("yyyy-MM-dd") + "~" + x.ScheduleDateTime.LastDayOfWeek().ToString("yyyy-MM-dd");
                        case "month":
                            DateTime date = x.ScheduleDateTime;
                            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            return firstDayOfMonth.ToString("yyyy-MM-dd") + "~" + lastDayOfMonth.ToString("yyyy-MM-dd");
                        default:
                            return x.ScheduleDateTime.ToString("yyyy-MM-dd");
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
            var model = db.EBMProjectSchedule.Select(x => new EBMScheduleViewModel
            {
                PSID = x.PSID.ToString(),
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                Target = x.Target,
                WokingHour = x.WokingHour,
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name,
                FinishDateTime = x.FinishDateTime,
                scheduleType = x.scheduleType.ToString(),
                ScheduleDateTime = x.ScheduleDateTime,
                ProgressingFlag = x.ProgressingFlag,
                Title = x.Title
            });
            var predicate = PredicateBuilder.New<EBMScheduleViewModel>(true);
            predicate = predicate.And(x => x.ScheduleDateTime >= para.start && x.ScheduleDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null && para.PIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMScheduleViewModel>(false);
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
}
