using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    [RoutePrefix("api/back/EBMProjectWorking")]
    public class EBMProjectWorkingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("test")]
        [HttpPost]
        public IHttpActionResult test()
        {
            return Ok("Received");
        }
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMProjectWorkingQueryModel model)
        {
            try
            {
                var dataset = EBMProjectWorkingViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMProjectWorkingViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "RecordDateTime descending" : "RecordDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.RecordDateTime >= model.Start && x.RecordDateTime <= model.End);
                if (!string.IsNullOrEmpty(model.PID))
                {
                    query = query.Where(x => x.PID == model.PID);
                }
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMProjectWorkingViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }

                var data = query;
                var result = new PagingViewModel<EBMProjectWorkingViewModel>()
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
    }
    public class EBMProjectWorkingViewModel
    {
        public EBMProjectWorkingViewModel()
        {

        }
        public static IQueryable<EBMProjectWorkingViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectWorking.Select(x => new EBMProjectWorkingViewModel()
            {
                PWID = x.PWID.ToString(),
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                Target = x.Target,
                Description = x.Description,
                WorkingHour = x.WokingHour,
                WorkingType = x.workingType.ToString(),
                RecordDateTime = x.RecordDateTime,
                Id = x.Id,
                UserName = x.ApplicationUser.UserName
            });
        }
        public string PWID { set; get; }

        public string PID { set; get; }

        public string ProjectName { set; get; }

        public string Target { set; get; }

        public string Description { set; get; }

        public decimal WorkingHour { set; get; }

        public string WorkingType { set; get; }

        public DateTime RecordDateTime { set; get; }

        public string Id { set; get; }

        public string UserName { set; get; }
    }
    public class EBMProjectWorkingQueryModel : PagingQueryModel
    {
        public string PID { set; get; }

        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }
    }
}
