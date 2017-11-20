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
    public class EBMProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EBMProjects
        public async Task<ActionResult> Index()
        {
            return View(await db.EBMProject.ToListAsync());
        }

        // GET: EBMProjects/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            if (eBMProject == null)
            {
                return HttpNotFound();
            }
            return View(eBMProject);
        }

        // GET: EBMProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EBMProjects/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PID,ProjectName,CreateDateTime,ProjectNo")] EBMProject eBMProject)
        {
            if (ModelState.IsValid)
            {
                eBMProject.PID = Guid.NewGuid();
                db.EBMProject.Add(eBMProject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eBMProject);
        }

        // GET: EBMProjects/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            if (eBMProject == null)
            {
                return HttpNotFound();
            }
            return View(eBMProject);
        }

        // POST: EBMProjects/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PID,ProjectName,CreateDateTime,ProjectNo")] EBMProject eBMProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBMProject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eBMProject);
        }

        // GET: EBMProjects/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            if (eBMProject == null)
            {
                return HttpNotFound();
            }
            return View(eBMProject);
        }

        // POST: EBMProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProject eBMProject = await db.EBMProject.FindAsync(id);
            db.EBMProject.Remove(eBMProject);
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
