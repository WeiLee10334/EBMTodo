using EBMTodo.Controllers.Api.Models;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Controllers.Api
{
    [RoutePrefix("api/EBMTodoList")]
    public class EBMTodoListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
                LineUID = x.EBMProjectMember.ApplicationUser.LineID,
                WorkerName = x.EBMProjectMember.ApplicationUser.FirstName
            }).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult create()
        {
            return BadRequest();
        }

    }
}
