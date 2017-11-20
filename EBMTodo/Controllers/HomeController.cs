using EBMTodo.ActionFilters;
using EBMTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using EBMTodo.Models.Todo;
using EBMTodo.Models.ViewModel;

namespace EBMTodo.Controllers
{
    [Authorize()]
    [Logger]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyWork()
        {
            var userId = User.Identity.GetUserId();
            var PMData = db.EBMProjectMember.Where(x => x.Id == userId).Select(x => x.PMID);
            var todoData = db.EBMProjectTodoList.Where(x => PMData.Contains(x.PMID));

            List<TodoListViewModel> data = new List<TodoListViewModel>();
            foreach (var item in todoData)
            {
                TodoListViewModel d = new TodoListViewModel();
                d.CreateDateTime = item.CreateDateTime;
                d.Title = item.title;
                d.Content = item.Description;
                d.CompleteRate = item.CompleteRate;
                    data.Add(d);
            }
            return PartialView("_MyWork", data);

        }


        public ActionResult Create()
        {
            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName");
            return PartialView("_Create");
        }

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
                return RedirectToAction("WorkList");
            }

            ViewBag.PID = new SelectList(db.EBMProject, "PID", "ProjectName", eBMProjectWorking.PID);
            return View(eBMProjectWorking);
        }

        public ActionResult WorkList()
        {
            var userId = User.Identity.GetUserId();
            var data = db.EBMProjectWorking.Where(x => x.Id == userId && x.CreateDateTime.Year >= DateTime.Now.Year && x.CreateDateTime.Month >= DateTime.Now.Month && x.CreateDateTime.Day >= DateTime.Now.Day).OrderByDescending(x=>x.CreateDateTime);
            return PartialView("_WorkList", data);

        }

        // POST: EBMProjectWorkings/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            EBMProjectWorking eBMProjectWorking = await db.EBMProjectWorking.FindAsync(id);
            db.EBMProjectWorking.Remove(eBMProjectWorking);
            await db.SaveChangesAsync();
            return RedirectToAction("WorkList");
        }

    }
}