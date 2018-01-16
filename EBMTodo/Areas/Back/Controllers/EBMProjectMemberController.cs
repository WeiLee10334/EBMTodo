using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
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
    [RoutePrefix("api/back/EBMProjectMember")]
    public class EBMProjectMemberController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMProjectMemberQueryModel model)
        {
            try
            {
                var dataset = EBMProjectMemberViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMProjectMemberViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "CreateDateTime descending" : "CreateDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.CreateDateTime >= model.Start && x.CreateDateTime <= model.End);
                if (!string.IsNullOrEmpty(model.PID))
                {
                    query = query.Where(x => x.PID == model.PID);
                }
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMProjectMemberViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string) && !string.IsNullOrEmpty(filter.Value))
                    {
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }

                var data = query;
                var result = new PagingViewModel<EBMProjectMemberViewModel>()
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
        public IHttpActionResult Create(EBMProjectMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = new EBMProjectMember()
                    {
                        title = model.title,
                        CreateDateTime = DateTime.Now,
                        PID = Guid.Parse(model.PID)
                    };
                    db.EBMProjectMember.Add(data);
                    db.SaveChanges();
                    model.PMID = data.PMID.ToString();
                    model.CreateDateTime = data.CreateDateTime;
                    return Ok(model);
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.NotAcceptable, e.InnerException.InnerException.Message);
                }
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "格式錯誤");
            }

        }
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(EBMProjectMemberViewModel model)
        {
            var data = db.EBMProjectMember.Find(Guid.Parse(model.PMID));
            if (data != null)
            {
                try
                {
                    data.title = model.title;
                    data.Id = model.Id;
                    data.PID = Guid.Parse(model.PID);
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
        public IHttpActionResult Delete(EBMProjectMemberViewModel model)
        {
            var data = db.EBMProjectMember.Find(Guid.Parse(model.PMID));
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
    public class EBMProjectMemberViewModel
    {
        public EBMProjectMemberViewModel()
        {

        }
        public static IQueryable<EBMProjectMemberViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProjectMember.Select(x => new EBMProjectMemberViewModel()
            {
                PMID = x.PMID.ToString(),
                CreateDateTime = x.CreateDateTime,
                title = x.title,
                Id = x.Id,
                UserName = x.ApplicationUser.UserName,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName
            });
        }
        public string PMID { set; get; }

        public DateTime? CreateDateTime { set; get; }
        [StringLength(100)]
        public string title { set; get; }

        public string Id { set; get; }

        public string UserName { set; get; }
        [Required]
        public string PID { set; get; }

        public string ProjectName { set; get; }
    }
    public class EBMProjectMemberQueryModel : PagingQueryModel
    {
        public string PID { set; get; }

        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }
    }
}
