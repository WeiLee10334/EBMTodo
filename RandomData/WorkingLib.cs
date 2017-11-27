using LinqKit;
using Newtonsoft.Json;
using RandomData.Models;
using RandomData.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomData
{
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
    public class WorkingLib
    {
        private EBMTodo db;
        public WorkingLib()
        {
            this.db = new EBMTodo();
        }
        public void initData()
        {

            var members = db.Line_User.Select(x => new
            {
                UID = x.UID,
                Name = x.Name
            }).ToList();

            var projects = db.TodoEBMProject.Select(x => new
            {
                PID = x.PID,
                ProjectName = x.ProjectName,
                ProjectNo = x.ProjectNo,
                CreateDateTime = x.CreateDateTime
            }).ToList();
            //return Ok(new { members = members, projects = projects });
        }

        public void getData(DateTime? start, DateTime? end, List<string> UIDs = null, string PID = null)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
            var model = db.TodoEBMProjectWorking.Select(x => new EBMPWorkingViewModel()
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.TodoEBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.Line_User.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            //var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            var predicate = PredicateBuilder.New<EBMPWorkingViewModel>(true);
            predicate = predicate.And(x => x.RecordDateTime >= start && x.RecordDateTime <= end);

            if (UIDs != null)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>();
                foreach (var id in UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList();
            foreach (var item in data)
            {
                Console.WriteLine(item.RecordDateTime + ":" + item.ProjectName + "-" + item.WorkerName);
            }
            //return Ok(data);
        }

        public void getDataByTime(DateTime? start, DateTime? end, string groupby = "day", string UID = null, string PID = null)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
            var model = db.TodoEBMProjectWorking.Select(x => new
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.TodoEBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.Line_User.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            query = UID == null ? query : query.Where(x => x.LineUID == UID);
            query = PID == null ? query : query.Where(x => x.PID == PID);


            var data = query.ToList()
                .GroupBy(x =>
                {
                    switch (groupby)
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
                            return "";
                    }
                })
               .Select(x => new
               {
                   date = x.Key,
                   list = x.ToList()
               }).OrderBy(x => x.date).ToList();
            foreach (var item in data)
            {
                Console.WriteLine(item.date + ":" + item.list.Count);
            }
        }

        public void getDataByUID(DateTime? start, DateTime? end, string UID = null, string PID = null)
        {
            //預設一個禮拜
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
            var model = db.TodoEBMProjectWorking.Select(x => new
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.TodoEBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.Line_User.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            query = UID == null ? query : query.Where(x => x.LineUID == UID);
            query = PID == null ? query : query.Where(x => x.PID == PID);

            var data = query.ToList()
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
            foreach (var item in data)
            {
                Console.WriteLine(item.workerName + ":" + item.list.Count);
            }
            //return Ok(data);
        }

    }
}
