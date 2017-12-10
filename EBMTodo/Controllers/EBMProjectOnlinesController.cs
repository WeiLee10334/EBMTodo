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
    public class EBMProjectOnlinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EBMProjectOnlines
        public async Task<ActionResult> Index()
        {
            var eBMProjectOnline = db.EBMProjectOnline.Include(e => e.EBMProjectMember);
            return View(await eBMProjectOnline.ToListAsync());
        }

        // GET: EBMProjectOnlines/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectOnline eBMProjectOnline = await db.EBMProjectOnline.FindAsync(id);
            if (eBMProjectOnline == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectOnline);
        }

        // GET: EBMProjectOnlines/Create
        public ActionResult Create()
        {
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title");
            return View();
        }

        // POST: EBMProjectOnlines/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "POID,CreateDateTime,ApplyDateTime,ApplyName,title,Description,CompleteRate,OnlineCategories,Memo,PMID")] EBMProjectOnline eBMProjectOnline)
        {
            if (ModelState.IsValid)
            {
                eBMProjectOnline.POID = Guid.NewGuid();
                db.EBMProjectOnline.Add(eBMProjectOnline);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectOnline.PMID);
            return View(eBMProjectOnline);
        }

        // GET: EBMProjectOnlines/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectOnline eBMProjectOnline = await db.EBMProjectOnline.FindAsync(id);
            if (eBMProjectOnline == null)
            {
                return HttpNotFound();
            }
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectOnline.PMID);
            return View(eBMProjectOnline);
        }

        // POST: EBMProjectOnlines/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "POID,CreateDateTime,ApplyDateTime,ApplyName,title,Description,CompleteRate,OnlineCategories,Memo,PMID")] EBMProjectOnline eBMProjectOnline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBMProjectOnline).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectOnline.PMID);
            return View(eBMProjectOnline);
        }

        // GET: EBMProjectOnlines/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectOnline eBMProjectOnline = await db.EBMProjectOnline.FindAsync(id);
            if (eBMProjectOnline == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectOnline);
        }

        // POST: EBMProjectOnlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProjectOnline eBMProjectOnline = await db.EBMProjectOnline.FindAsync(id);
            db.EBMProjectOnline.Remove(eBMProjectOnline);
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
