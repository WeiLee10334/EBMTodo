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

namespace EBMTodo.Controllers
{
    public class EBMProjectSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EBMProjectSchedules
        public async Task<ActionResult> Index()
        {
            var eBMProjectSchedules = db.EBMProjectSchedule.Include(e => e.ApplicationUser).Include(e => e.EBMProject);
            return View(await eBMProjectSchedules.ToListAsync());
        }

        // GET: EBMProjectSchedules/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectSchedule eBMProjectSchedule = await db.EBMProjectSchedule.FindAsync(id);
            if (eBMProjectSchedule == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectSchedule);
        }

        // GET: EBMProjectSchedules/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName");
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName");
            return View();
        }

        // POST: EBMProjectSchedules/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PSID,CreateDateTime,ScheduleDateTime,Target,Description,WokingHour,scheduleType,FinishDateTime,LineUID,Title,ProgressingFlag,Id,PID")] EBMProjectSchedule eBMProjectSchedule)
        {
            if (ModelState.IsValid)
            {
                eBMProjectSchedule.PSID = Guid.NewGuid();
                db.EBMProjectSchedule.Add(eBMProjectSchedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectSchedule.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectSchedule.PID);
            return View(eBMProjectSchedule);
        }

        // GET: EBMProjectSchedules/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectSchedule eBMProjectSchedule = await db.EBMProjectSchedule.FindAsync(id);
            if (eBMProjectSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectSchedule.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectSchedule.PID);
            return View(eBMProjectSchedule);
        }

        // POST: EBMProjectSchedules/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PSID,CreateDateTime,ScheduleDateTime,Target,Description,WokingHour,scheduleType,FinishDateTime,LineUID,Title,ProgressingFlag,Id,PID")] EBMProjectSchedule eBMProjectSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBMProjectSchedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectSchedule.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectSchedule.PID);
            return View(eBMProjectSchedule);
        }

        // GET: EBMProjectSchedules/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectSchedule eBMProjectSchedule = await db.EBMProjectSchedule.FindAsync(id);
            if (eBMProjectSchedule == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectSchedule);
        }

        // POST: EBMProjectSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProjectSchedule eBMProjectSchedule = await db.EBMProjectSchedule.FindAsync(id);
            db.EBMProjectSchedule.Remove(eBMProjectSchedule);
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
