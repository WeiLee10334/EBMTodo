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
    public class EBMPWorkingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IHttpActionResult getData(string filter = null, string keyword = null, string orderby = null, int skip = 0, int length = 20)
        {
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel()
            {
                PWID = x.PWID,
                CreateDateTime = x.CreateDateTime,
                Description = x.Description,
                LineUID = x.LineUID,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType
            });
            var query = model;
            var data = query.Skip(skip).Take(length).ToList();
            return Ok(data);
        }
        public IHttpActionResult getDataByTime(DateTime start, DateTime end, string filter = null, string keyword = null, string orderby = null)
        {
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel()
            {
                PWID = x.PWID,
                CreateDateTime = x.CreateDateTime,
                Description = x.Description,
                LineUID = x.LineUID,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType
            }).Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            var query = model;
            var data = query.ToList();
            return Ok(data);
        }
    }
}
