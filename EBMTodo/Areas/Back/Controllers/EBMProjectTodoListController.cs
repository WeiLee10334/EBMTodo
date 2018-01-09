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
    [RoutePrefix("api/back/EBMProjectTodoList")]
    public class EBMProjectTodoListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMTodoListQueryModel model)
        {
            try
            {
                var dataset = EBMTodoListViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMTodoListViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "ApplyDateTime descending" : "ApplyDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.ApplyDateTime >= model.Start && x.ApplyDateTime <= model.End);
                if (!string.IsNullOrEmpty(model.PID))
                {
                    query = query.Where(x => x.PID == model.PID);
                }
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMTodoListViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }
                var data = query;
                var result = new PagingViewModel<EBMTodoListViewModel>()
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
        public IHttpActionResult Create(EBMTodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = new EBMProjectTodoList()
                    {
                        title = model.title,
                        CreateDateTime = DateTime.Now,
                        ApplyDateTime = model.ApplyDateTime,
                        ApplyName = model.ApplyName,
                        Description = model.Description,
                        PMID = Guid.Parse(model.PMID),
                        CompleteRate = model.CompleteRate,
                        Tag = model.Tag,
                        Memo = model.Memo
                    };
                    db.EBMProjectTodoList.Add(data);
                    db.SaveChanges();
                    model.PTLID = data.PTLID.ToString();
                    model.ApplyDateTime = data.ApplyDateTime;
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
        public IHttpActionResult Update(EBMTodoListViewModel model)
        {
            var data = db.EBMProjectTodoList.Find(Guid.Parse(model.PTLID));
            if (data != null)
            {
                try
                {
                    data.title = model.title;
                    data.ApplyDateTime = model.ApplyDateTime;
                    data.ApplyName = model.ApplyName;
                    data.CompleteRate = model.CompleteRate;
                    data.Description = model.Description;
                    data.Tag = model.Tag;
                    data.Memo = model.Memo;
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
        public IHttpActionResult Delete(EBMTodoListViewModel model)
        {
            var data = db.EBMProjectTodoList.Find(Guid.Parse(model.PTLID));
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
    public class EBMTodoListViewModel
    {
        public EBMTodoListViewModel()
        {

        }
        public static IQueryable<EBMTodoListViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectTodoList.Select(x => new EBMTodoListViewModel()
            {
                PTLID = x.PTLID.ToString(),
                ApplyDateTime = x.ApplyDateTime,
                ApplyName = x.ApplyName,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                PMID = x.PMID.ToString(),
                MemberTitle = x.EBMProjectMember == null ? null : x.EBMProjectMember.title,
                Memo = x.Memo,
                Tag = x.Tag,
                PID = x.EBMProjectMember == null ? null : x.EBMProjectMember.PID.ToString()
            });
        }
        public string PTLID { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyName { set; get; }

        public string title { set; get; }

        public string Description { set; get; }

        public int CompleteRate { set; get; }

        public string PMID { set; get; }

        public string MemberTitle { set; get; }

        public string Tag { set; get; }

        public string Memo { set; get; }

        public string PID { set; get; }
    }
    public class EBMTodoListQueryModel : PagingQueryModel
    {
        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }

        public string PID { set; get; }
    }
}
