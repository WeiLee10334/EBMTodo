using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/ebmpworking")]
    public class EBMPWorkingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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

        [Route("getData")]
        public IHttpActionResult getData(DateTime? start, DateTime? end, string UID = null, string PID = null)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
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
            var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            query = UID == null ? query : query.Where(x => x.LineUID == UID);
            query = PID == null ? query : query.Where(x => x.PID == PID);
            var data = query.ToList();
            return Ok(data);
        }
        [Route("getDataByTime")]
        public IHttpActionResult getDataByTime(DateTime? start, DateTime? end, int groupby = 1, string UID = null, string PID = null)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
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
            var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            query = UID == null ? query : query.Where(x => x.LineUID == UID);
            query = PID == null ? query : query.Where(x => x.PID == PID);


            var data = query.ToList()
                .GroupBy(x =>
                {
                    switch (groupby)
                    {
                        case 1:
                            return x.RecordDateTime.ToString("yyyy-MM-dd");
                        case 2:
                            //x.RecordDateTime.Ticks / TimeSpan.FromHours(1).Ticks
                            return x.RecordDateTime.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");
                        case 3:
                            return x.RecordDateTime.ToString("yyyy-MM-01");
                        default:
                            break;
                    }

                    return "";
                })
               .Select(x => new
               {
                   date = x.Key,
                   list = x.ToList()
               }).ToList();
            return Ok(data);
        }
        [Route("getDataByUID")]
        public IHttpActionResult getDataByUID(DateTime? start, DateTime? end, string UID = null, string PID = null)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
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
                   LineUID = x.Key.UID,
                   WorkerName = x.Key.Name,
                   List = x.ToList()
               }).ToList();
            return Ok(data);
        }
    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
