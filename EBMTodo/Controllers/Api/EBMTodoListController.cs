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
                CreateDateTime = x.CreateDateTime,
                title = x.title,
                Description = x.Description,
                CompleteRate = x.CompleteRate,
                PMID = x.PMID.ToString(),
                MemberTitle = x.EBMProjectMember.title
            }).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult create(EBMTodoListViewModel model)
        {
            var data = new EBMProjectTodoList()
            {
                title = model.title,
                CreateDateTime = DateTime.Now,
                Description = model.Description,
                PMID = Guid.Parse(model.PMID),
                CompleteRate = 0
            };
            db.EBMProjectTodoList.Add(data);
            db.SaveChanges();
            return Ok();
        }

    }
}
