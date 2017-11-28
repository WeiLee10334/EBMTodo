using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBMTodo.Models;
using EBMTodo.Models.Todo;
using Microsoft.AspNet.Identity;
using EBMTodo.Controllers.Api.Models;
using Newtonsoft.Json;
using LinqKit;
using EBMTodo.Controllers.Api;

namespace EBMTodo.Controllers
{
    public class EBMProjectWorkingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.memberList = db.LineUser.ToList();
            ViewBag.projectList = db.EBMProject.ToList();
            return View();
        }
        public ActionResult getData(DateTime? start, DateTime? end, string[] UIDs, string[] PIDs)
        {
            start = start == null ? DateTime.Now.Date.AddDays(-7) : start.Value.Date;
            end = end == null ? DateTime.Now.Date.AddDays(1) : end.Value.Date.AddDays(1);
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var query = model.Where(x => x.RecordDateTime >= start && x.RecordDateTime <= end);
            if (UIDs != null && UIDs.Length > 0)
            {
                var id0 = UIDs[0];
                query = query.Where(x => x.LineUID == id0);
                for (int i = 1; i < UIDs.Length; i++)
                {
                    var id = UIDs[i];
                    query = query.Union(query.Where(x => x.LineUID == id));
                }
            }
            if (PIDs != null && PIDs.Length > 0)
            {
                var id0 = PIDs[0];
                query = query.Where(x => x.PID == id0);
                for (int i = 1; i < PIDs.Length; i++)
                {
                    var id = UIDs[i];
                    query = query.Union(query.Where(x => x.PID == id));
                }
            }
            var data = query.ToList();
            return Content(JsonConvert.SerializeObject(data));
        }
        public ActionResult getDataByTime(WorkingQueryModel para)
        {
            para.start = para.start == null ? DateTime.Now.Date.AddDays(-7) : para.start.Value.Date;
            para.end = para.end == null ? DateTime.Now.Date.AddDays(1) : para.end.Value.Date.AddDays(1);
            var model = db.EBMProjectWorking.Select(x => new EBMPWorkingViewModel
            {
                PWID = x.PWID,
                Description = x.Description,
                LineUID = x.LineUID,
                PID = x.PID.ToString(),
                ProjectName = x.EBMProject.ProjectName,
                RecordDateTime = x.RecordDateTime,
                Target = x.Target,
                WokingHour = x.WokingHour,
                workingType = x.workingType.ToString(),
                WorkerName = db.LineUser.FirstOrDefault(y => y.UID == x.LineUID).Name
            });
            var predicate = PredicateBuilder.New<EBMPWorkingViewModel>(true);
            predicate = predicate.And(x => x.RecordDateTime >= para.start && x.RecordDateTime <= para.end);

            if (para.UIDs != null && para.UIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>();
                foreach (var id in para.UIDs)
                {
                    orPredicate = orPredicate.Or(x => x.LineUID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            if (para.PIDs != null && para.PIDs.Count > 0)
            {
                var orPredicate = PredicateBuilder.New<EBMPWorkingViewModel>(false);
                foreach (var id in para.PIDs)
                {
                    orPredicate = orPredicate.Or(x => x.PID == id);
                }
                predicate = predicate.And(orPredicate);
            }

            var data = model.Where(predicate).ToList()
                .GroupBy(x =>
                {
                    switch (para.groupby)
                    {
                        case "day":
                            return x.RecordDateTime.ToString("yyyy-MM-dd");
                        case "week":
                            return x.RecordDateTime.FirstDayOfWeek().ToString("yyyy-MM-dd") + "~" + x.RecordDateTime.LastDayOfWeek().ToString("yyyy-MM-dd");
                        case "month":
                            DateTime date = x.RecordDateTime;
                            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            return firstDayOfMonth.ToString("yyyy-MM-dd") + "~" + lastDayOfMonth.ToString("yyyy-MM-dd");
                        default:
                            return x.RecordDateTime.ToString("yyyy-MM-dd");
                    }
                })
               .Select(x => new
               {
                   date = x.Key,
                   list = x.ToList()
               }).OrderBy(x => x.date).ToList();
            return PartialView("_groupbytime", data);
        }
        // GET: EBMProjectWorkings/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectWorking eBMProjectWorking = await db.EBMProjectWorking.FindAsync(id);
            if (eBMProjectWorking == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectWorking);
        }

        // GET: EBMProjectWorkings/Create
        public ActionResult Create()
        {
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName");
            return View();
        }

        // POST: EBMProjectWorkings/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PWID,CreateDateTime,Description,WokingHour,PID,workingType")] EBMProjectWorking eBMProjectWorking)
        {
            ModelState.Remove("Id");
            eBMProjectWorking.Id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                eBMProjectWorking.PWID = Guid.NewGuid();
                db.EBMProjectWorking.Add(eBMProjectWorking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectWorking.PID);
            return View(eBMProjectWorking);
        }

        // GET: EBMProjectWorkings/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectWorking eBMProjectWorking = await db.EBMProjectWorking.FindAsync(id);
            if (eBMProjectWorking == null)
            {
                return HttpNotFound();
            }
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectWorking.PID);
            return View(eBMProjectWorking);
        }

        // POST: EBMProjectWorkings/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PWID,CreateDateTime,Description,WokingHour,PID")] EBMProjectWorking eBMProjectWorking)
        {
            ModelState.Remove("Id");
            eBMProjectWorking.Id = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(eBMProjectWorking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectWorking.PID);
            return View(eBMProjectWorking);
        }

        // GET: EBMProjectWorkings/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectWorking eBMProjectWorking = await db.EBMProjectWorking.FindAsync(id);
            if (eBMProjectWorking == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectWorking);
        }

        // POST: EBMProjectWorkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProjectWorking eBMProjectWorking = await db.EBMProjectWorking.FindAsync(id);
            db.EBMProjectWorking.Remove(eBMProjectWorking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
