using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/ebmtodoList")]
    public class EBMTodoListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [Route("initData")]
        public IHttpActionResult initData()
        {
            var members = db.EBMProjectMember.Select(x => new
            {
                id = x.PMID,
                name = x.title
            }).ToList();
            return Ok(members);
        }
        [HttpPost]
        [Route("getData")]
        public IHttpActionResult getData()
        {
            var data = db.EBMProjectTodoList.Select(x => new EBMTodoListViewModel
            {
                PTLID = x.PTLID.ToString(),
                ApplyDateTime = x.ApplyDateTime,
                ApplyName = x.ApplyName,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                PMID = x.PMID.ToString(),
                MemberTitle = x.EBMProjectMember.title,
                Memo = x.Memo,
                Tag = x.Tag
            }).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public IHttpActionResult CreateOrUpdate(EBMTodoListViewModel model)
        {
            if (string.IsNullOrEmpty(model.PTLID))
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
                model = db.EBMProjectTodoList.Select(x => new EBMTodoListViewModel
                {
                    PTLID = x.PTLID.ToString(),
                    ApplyDateTime = x.ApplyDateTime,
                    ApplyName = x.ApplyName,
                    title = x.title,
                    Description = x.Description,
                    CompleteRate = x.CompleteRate,
                    PMID = x.PMID.ToString(),
                    MemberTitle = x.EBMProjectMember.title,
                    Memo = x.Memo,
                    Tag = x.Tag
                }).FirstOrDefault(x => x.PTLID == data.PTLID.ToString());
                return Ok(model);
            }
            else
            {
                var target = db.EBMProjectTodoList.Find(Guid.Parse(model.PTLID));
                target.title = model.title;
                target.ApplyDateTime = model.ApplyDateTime;
                target.ApplyName = model.ApplyName;
                target.CompleteRate = model.CompleteRate;
                target.Description = model.Description;
                target.Tag = model.Tag;
                target.PMID = Guid.Parse(model.PMID);
                target.Memo = model.Memo;
                db.Entry(target).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                model.MemberTitle = target.EBMProjectMember.title;
                return Ok(model);
            }
        }

    }
    public class TodoQueryModel
    {
        public int skip { set; get; }

        public int length { set; get; }

        public string orderby { set; get; }

    }
}
