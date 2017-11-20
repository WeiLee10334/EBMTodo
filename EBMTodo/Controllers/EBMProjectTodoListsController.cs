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
    public class EBMProjectTodoListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EBMProjectTodoLists
        public async Task<ActionResult> Index()
        {
            var eBMProjectTodoList = db.EBMProjectTodoList.Include(e => e.EBMProjectMember);
            return View(await eBMProjectTodoList.ToListAsync());
        }

        // GET: EBMProjectTodoLists/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectTodoList eBMProjectTodoList = await db.EBMProjectTodoList.FindAsync(id);
            if (eBMProjectTodoList == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectTodoList);
        }

        // GET: EBMProjectTodoLists/Create
        public ActionResult Create()
        {
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title");
            return View();
        }

        // POST: EBMProjectTodoLists/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PTLID,CreateDateTime,title,Description,CompleteRate,PMID")] EBMProjectTodoList eBMProjectTodoList)
        {
            if (ModelState.IsValid)
            {
                eBMProjectTodoList.PTLID = Guid.NewGuid();
                db.EBMProjectTodoList.Add(eBMProjectTodoList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectTodoList.PMID);
            return View(eBMProjectTodoList);
        }

        // GET: EBMProjectTodoLists/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectTodoList eBMProjectTodoList = await db.EBMProjectTodoList.FindAsync(id);
            if (eBMProjectTodoList == null)
            {
                return HttpNotFound();
            }
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectTodoList.PMID);
            return View(eBMProjectTodoList);
        }

        // POST: EBMProjectTodoLists/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PTLID,CreateDateTime,title,Description,CompleteRate,PMID")] EBMProjectTodoList eBMProjectTodoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBMProjectTodoList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PMID = new SelectList(db.EBMProjectMember, "PMID", "title", eBMProjectTodoList.PMID);
            return View(eBMProjectTodoList);
        }

        // GET: EBMProjectTodoLists/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBMProjectTodoList eBMProjectTodoList = await db.EBMProjectTodoList.FindAsync(id);
            if (eBMProjectTodoList == null)
            {
                return HttpNotFound();
            }
            return View(eBMProjectTodoList);
        }

        // POST: EBMProjectTodoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EBMProjectTodoList eBMProjectTodoList = await db.EBMProjectTodoList.FindAsync(id);
            db.EBMProjectTodoList.Remove(eBMProjectTodoList);
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
