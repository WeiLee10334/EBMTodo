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
using EBMTodo.Models.Base;

namespace EBMTodo.Controllers
{
    [Authorize(Roles = "UnitManage")]

    public class RBUnitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RBUnits
        public async Task<ActionResult> Index()
        {
            return View(await db.RBUnit.ToListAsync());
        }

        // GET: RBUnits/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RBUnit rBUnit = await db.RBUnit.FindAsync(id);
            if (rBUnit == null)
            {
                return HttpNotFound();
            }
            return View(rBUnit);
        }

        // GET: RBUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RBUnits/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UID,UnitName,CDAT,ZipCode,Address1,Address2,PhoneNumber,Fax,UnitLevel")] RBUnit rBUnit)
        {
            if (ModelState.IsValid)
            {
                db.RBUnit.Add(rBUnit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rBUnit);
        }

        // GET: RBUnits/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RBUnit rBUnit = await db.RBUnit.FindAsync(id);
            if (rBUnit == null)
            {
                return HttpNotFound();
            }
            return View(rBUnit);
        }

        // POST: RBUnits/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UID,UnitName,CDAT,ZipCode,Address1,Address2,PhoneNumber,Fax,UnitLevel")] RBUnit rBUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rBUnit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rBUnit);
        }

        // GET: RBUnits/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RBUnit rBUnit = await db.RBUnit.FindAsync(id);
            if (rBUnit == null)
            {
                return HttpNotFound();
            }
            return View(rBUnit);
        }

        // POST: RBUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            RBUnit rBUnit = await db.RBUnit.FindAsync(id);
            db.RBUnit.Remove(rBUnit);
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
