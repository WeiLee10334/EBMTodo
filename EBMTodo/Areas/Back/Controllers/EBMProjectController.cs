﻿using EBMTodo.Areas.Back.Models;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EBMTodo.Areas.Back.Controllers
{
    [RoutePrefix("api/back/EBMProject")]
    public class EBMProjectController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private void LogInfo(string pMessage)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Logfiles/");
            string tFilePath = path + "Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileStream fs;
            if (!Directory.Exists(path))
            {
                //新增資料夾
                Directory.CreateDirectory(path);
            }
            if (!System.IO.File.Exists(tFilePath))
            {
                fs = new FileStream(tFilePath, FileMode.Create);
            }
            else
            {
                fs = new FileStream(tFilePath, FileMode.Append);
            }
            pMessage = DateTime.Now.ToString("HH:mm:ss    ") + pMessage;
            StreamWriter sw = new StreamWriter(fs);
            //開始寫入
            sw.WriteLine(pMessage);
            //清空緩衝區
            sw.Flush();
            //關閉流
            sw.Close();
            fs.Close();

        }
        [Route("GetList")]
        [HttpPost]
        public IHttpActionResult GetList(EBMProjectQueryModel model)
        {
            try
            {
                var dataset = EBMProjectViewModel.GetQueryable(db);
                model.Start = model.Start == null ? DateTime.MinValue : model.Start;
                model.End = model.End == null ? DateTime.MaxValue : model.End;
                model.OrderBy = typeof(EBMProjectViewModel).GetProperty(model.OrderBy) == null ?
                (model.Reverse ? "CreateDateTime descending" : "CreateDateTime") :
                (model.Reverse ? model.OrderBy + " descending" : model.OrderBy);
                var query = dataset.Where(x => x.CreateDateTime >= model.Start && x.CreateDateTime <= model.End);
                foreach (var filter in model.Filters)
                {
                    var prop = typeof(EBMProjectViewModel).GetProperty(filter.Key);
                    if (prop != null && prop.PropertyType == typeof(string) && !string.IsNullOrEmpty(filter.Value))
                    {
                        LogInfo(filter.Key + ":" + filter.Value);
                        query = query.Where(filter.Key + ".Contains(@0)", filter.Value);
                    }
                }
                var data = query;
                var result = new PagingViewModel<EBMProjectViewModel>()
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
        public IHttpActionResult Create(EBMProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = new EBMProject()
                    {
                        CreateDateTime = DateTime.Now,
                        IsHode = model.IsHode,
                        ProjectName = model.ProjectName,
                        ProjectNo = model.ProjectNo
                    };
                    db.EBMProject.Add(data);
                    db.SaveChanges();
                    model.PID = data.PID.ToString();
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
        public IHttpActionResult Update(EBMProjectViewModel model)
        {
            var data = db.EBMProject.Find(Guid.Parse(model.PID));
            if (data != null)
            {
                try
                {
                    data.ProjectName = model.ProjectName;
                    data.IsHode = model.IsHode;
                    data.ProjectNo = model.ProjectNo;
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
        public IHttpActionResult Delete(EBMProjectViewModel model)
        {
            var data = db.EBMProject.Find(Guid.Parse(model.PID));
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
    public class EBMProjectViewModel
    {
        public EBMProjectViewModel()
        {

        }
        public static IQueryable<EBMProjectViewModel> GetQueryable(ApplicationDbContext context)
        {
            return context.EBMProject.Select(x => new EBMProjectViewModel()
            {
                PID = x.PID.ToString(),
                ProjectName = x.ProjectName,
                CreateDateTime = x.CreateDateTime,
                IsHode = x.IsHode,
                ProjectNo = x.ProjectNo,
            });
        }
        public string PID { set; get; }
        [StringLength(100)]
        public string ProjectName { set; get; }

        public DateTime? CreateDateTime { set; get; }
        [StringLength(100)]
        public string ProjectNo { set; get; }

        public bool IsHode { set; get; }
    }
    public class EBMProjectQueryModel : PagingQueryModel
    {
        public DateTime? Start { set; get; }

        public DateTime? End { set; get; }
    }
}
