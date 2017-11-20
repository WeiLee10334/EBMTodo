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
    public class EBMProjectMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EBMProjectMembers
        public async Task<ActionResult> Index()
        {
            var eBMProjectMember = db.EBMProjectMember.Include(e => e.ApplicationUser).Include(e => e.EBMProject);
            return View(await eBMProjectMember.ToListAsync());
        }

        // GET: EBMProjectMembers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectMember eBMProjectMember = await db.EBMProjectMember.FindAsync(id);
            if (eBMProjectMember == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectMember);
        }

        // GET: EBMProjectMembers/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName");
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName");
            return View();
        }

        // POST: EBMProjectMembers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PMID,CreateDateTime,title,Id,PID")] EBMProjectMember eBMProjectMember)
        {
            if (ModelState.IsValid)
            {
                eBMProjectMember.PMID = Guid.NewGuid();
                db.EBMProjectMember.Add(eBMProjectMember);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectMember.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectMember.PID);
            return View(eBMProjectMember);
        }

        // GET: EBMProjectMembers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectMember eBMProjectMember = await db.EBMProjectMember.FindAsync(id);
            if (eBMProjectMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectMember.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectMember.PID);
            return View(eBMProjectMember);
        }

        // POST: EBMProjectMembers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PMID,CreateDateTime,title,Id,PID")] EBMProjectMember eBMProjectMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBMProjectMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "LastName", eBMProjectMember.Id);
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectMember.PID);
            return View(eBMProjectMember);
        }

        // GET: EBMProjectMembers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectMember eBMProjectMember = await db.EBMProjectMember.FindAsync(id);
            if (eBMProjectMember == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectMember);
        }

        // POST: EBMProjectMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProjectMember eBMProjectMember = await db.EBMProjectMember.FindAsync(id);
            db.EBMProjectMember.Remove(eBMProjectMember);
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
