using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    [RoutePrefix("api/back/EBMProjectSchedule")]
    public class EBMProjectScheduleController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMProjectScheduleQueryModel model)
        {
            try
            {
                var dataset = EBMProjectScheduleViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMProjectScheduleViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "ScheduleDateTime descending" : "ScheduleDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.ScheduleDateTime >= model.Start && x.ScheduleDateTime <= model.End);
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMProjectScheduleViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string) && !string.IsNullOrEmpty(filter.Value))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }
                var data = query;
                var result = new PagingViewModel<EBMProjectScheduleViewModel>()
                {
                    Skip = model.Skip,
                    Length = model.Length,
                    Total = data.Count(),
                    Data = data.OrderBy(model.OrderBy).Skip(model.Skip).Take(model.Length).ToList()
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(EBMProjectScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = new EBMProjectSchedule()
                    {
                        CreateDateTime = DateTime.Now,
                        Title = model.Title,
                        scheduleType = model.scheduleType,
                        ScheduleDateTime = model.ScheduleDateTime,
                        Description = model.Description,
                        PID = Guid.Parse(model.PID),
                        Id = model.Id,
                        Target = model.Target,
                        FinishDateTime = model.FinishDateTime,
                        WokingHour = model.WokingHour,
                        ProgressingFlag = model.ProgressingFlag
                    };
                    db.EBMProjectSchedule.Add(data);
                    db.SaveChanges();
                    model.PSID = data.PSID.ToString();
                    return Ok(model);
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
            }

        }
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(EBMProjectScheduleViewModel model)
        {
            var data = db.EBMProjectSchedule.Find(Guid.Parse(model.PSID));
            if (data != null)
            {
                try
                {
                    data.CreateDateTime = DateTime.Now;
                    data.Title = model.Title;
                    data.scheduleType = model.scheduleType;
                    data.ScheduleDateTime = model.ScheduleDateTime;
                    data.Description = model.Description;
                    data.PID = Guid.Parse(model.PID);
                    data.Id = model.Id;
                    data.Target = model.Target;
                    data.FinishDateTime = model.FinishDateTime;
                    data.WokingHour = model.WokingHour;
                    data.ProgressingFlag = model.ProgressingFlag;
                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(model);
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            return BadRequest("not exist");
        }
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult Delete(EBMProjectScheduleViewModel model)
        {
            var data = db.EBMProjectSchedule.Find(Guid.Parse(model.PSID));
            if (data != null)
            {
                try
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.Message);
                }
            }
            return BadRequest("not exist");
        }

    }
    public class EBMProjectScheduleViewModel
    {
        public EBMProjectScheduleViewModel()
        {

        }
        public static IQueryable<EBMProjectScheduleViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectSchedule.Select(x => new EBMProjectScheduleViewModel()
            {
                PSID = x.PSID.ToString(),
                ScheduleDateTime = x.ScheduleDateTime,
                Target = x.Target,
                Description = x.Description,
                WokingHour = x.WokingHour,
                scheduleType = x.scheduleType,
                FinishDateTime = x.FinishDateTime,
                LineUID = x.LineUID,
                Title = x.Title,
                ProgressingFlag = x.ProgressingFlag,
                Id = x.Id,
                PID = x.PID.ToString()
            });
        }

        public string PSID { set; get; }

        public DateTime ScheduleDateTime { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WokingHour { set; get; }

        public ScheduleType scheduleType { set; get; }

        public DateTime FinishDateTime { set; get; }

        public string LineUID { set; get; }

        public string Title { set; get; }

        public bool ProgressingFlag { set; get; }

        public string Id { set; get; }
        public string UserName { set; get; }

        public string PID { set; get; }
    }
    public class EBMProjectScheduleQueryModel : PagingQueryModel
    {
        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }
    }
}
