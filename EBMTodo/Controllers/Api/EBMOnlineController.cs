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
            }).OrderByDescending(x => x.CreateDateTime).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult Insert(EBMOnlineViewModel model)
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
                return Ok(model);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }

        }
        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(EBMOnlineViewModel model)
        {
            try
            {
                Guid key = Guid.Parse(model.POID);
                var target = db.EBMProjectOnline.FirstOrDefault(x => x.POID == key);
                target.ApplyDateTime = model.ApplyDateTime;
                target.ApplyDepartment = model.ApplyDepartment;
                target.ApplyName = model.ApplyName;
                target.CompleteRate = model.CompleteRate;
                target.Description = model.Description;
                target.HandleDateTime = model.HandleDateTime;
                target.HandleName = model.HandleName;
                target.OnlineCategories = model.OnlineCategories;
                target.ResolveDateTime = model.ResolveDateTime;
                target.ResponseName = model.ResponseName;
                target.title = model.title;
                db.Entry(target).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(model);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }
        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(EBMOnlineViewModel model)
        {
            Guid key = Guid.Parse(model.POID);
            var target = db.EBMProjectOnline.FirstOrDefault(x => x.POID == key);
            db.Entry(target).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
