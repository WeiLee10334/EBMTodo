using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using EBMTodo.Models.Base.Enum;
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
    [RoutePrefix("api/back/EBMProjectOnline")]
    public class EBMProjectOnlineController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(PagingQueryModel model)
        {
            try
            {
                var dataset = EBMProjectOnlineViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMProjectOnlineViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "CreateDateTime descending" : "CreateDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.CreateDateTime >= model.Start && x.CreateDateTime <= model.End);
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMProjectOnlineViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string) && !string.IsNullOrEmpty(filter.Value))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }
                var data = query;
                var result = new PagingViewModel<EBMProjectOnlineViewModel>()
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
        public IHttpActionResult Create(EBMProjectOnlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = new EBMProjectOnline()
                    {
                        ApplyDateTime = model.ApplyDateTime,
                        ApplyDepartment = model.ApplyDepartment,
                        ApplyName = model.ApplyName,
                        CompleteRate = model.CompleteRate,
                        CreateDateTime = DateTime.Now,
                        Description = model.Description,
                        HandleDateTime = model.HandleDateTime,
                        HandleName = model.HandleName,
                        OnlineCategories = model.OnlineCategories,
                        ResolveDateTime = model.ResolveDateTime,
                        ResponseName = model.ResponseName,
                        title = model.title
                    };
                    db.EBMProjectOnline.Add(data);
                    db.SaveChanges();
                    model.POID = data.POID.ToString();
                    model.CreateDateTime = data.CreateDateTime;
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
        public IHttpActionResult Update(EBMProjectOnlineViewModel model)
        {
            var data = db.EBMProjectOnline.Find(Guid.Parse(model.POID));
            if (data != null)
            {
                try
                {
                    data.ApplyDateTime = model.ApplyDateTime;
                    data.ApplyDepartment = model.ApplyDepartment;
                    data.ApplyName = model.ApplyName;
                    data.CompleteRate = model.CompleteRate;
                    data.Description = model.Description;
                    data.HandleDateTime = model.HandleDateTime;
                    data.HandleName = model.HandleName;
                    data.OnlineCategories = model.OnlineCategories;
                    data.ResolveDateTime = model.ResolveDateTime;
                    data.ResponseName = model.ResponseName;
                    data.title = model.title;
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
        public IHttpActionResult Delete(EBMProjectOnlineViewModel model)
        {
            var data = db.EBMProjectOnline.Find(Guid.Parse(model.POID));
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
    public class EBMProjectOnlineViewModel
    {
        public EBMProjectOnlineViewModel()
        {

        }
        public static IQueryable<EBMProjectOnlineViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectOnline.Select(x => new EBMProjectOnlineViewModel()
            {
                POID = x.POID.ToString(),
                ApplyDateTime = x.ApplyDateTime,
                ApplyName = x.ApplyName,
                HandleDateTime = x.HandleDateTime,
                HandleName = x.HandleName,
                ResolveDateTime = x.ResolveDateTime,
                ResponseName = x.ResponseName,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                ApplyDepartment = x.ApplyDepartment,
                CreateDateTime = x.CreateDateTime,
                Memo = x.Memo,
                OnlineCategories = x.OnlineCategories,
            });
        }
        public string POID { set; get; }

        public DateTime CreateDateTime { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyDepartment { set; get; }

        public string ApplyName { set; get; }

        public string Description { set; get; }

        public DateTime? HandleDateTime { set; get; }

        public string HandleName { set; get; }

        public DateTime? ResolveDateTime { set; get; }

        public string ResponseName { set; get; }

        public string Memo { set; get; }

        public string title { set; get; }

        public int CompleteRate { set; get; }

        public OnlineCategories OnlineCategories { set; get; }
    }
}
