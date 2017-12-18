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
    [RoutePrefix("api/ebmonline")]
    public class EBMOnlineController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [Route("getData")]
        public IHttpActionResult getData()
        {
            var data = db.EBMProjectOnline.Select(x => new EBMOnlineViewModel
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
            }).ToList();
            return Ok(data);
        }
    }
}
