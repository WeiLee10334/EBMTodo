using EBMTodo.Models;
using EBMTodo.Models.UserPermission;
using EBMTodo.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace EBMTodo.Controllers
{
    [Authorize(Roles = "AccountManage")]

    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // Add the Group Manager (NOTE: only access through the public
        // Property, not by the instance variable!)
        private ApplicationGroupManager _groupManager;
        public ApplicationGroupManager GroupManager
        {
            get
            {
                return _groupManager ?? new ApplicationGroupManager();
            }
            private set
            {
                _groupManager = value;
            }
        }
        // Add the Unit Manager (NOTE: only access through the public
        // Property, not by the instance variable!)
        private ApplicationUnitManager _unitManager;
        public ApplicationUnitManager UnitManager
        {
            get
            {
                return _unitManager ?? new ApplicationUnitManager();
            }
            private set
            {
                _unitManager = value;
            }
        }
        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            // Show a list of available groups:
            ViewBag.GroupsList =
                new SelectList(this.GroupManager.Groups, "Id", "Name");
            ViewBag.UnitsList =
            new SelectList(this.UnitManager.Units, "Id", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel,  string[] selectedGroups, string[] selectedUnit)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    // Add the Address Info:
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Groups 
                if (adminresult.Succeeded)
                {
                    if (selectedGroups != null)
                    {
                        selectedGroups = selectedGroups ?? new string[] { };
                        await this.GroupManager
                            .SetUserGroupsAsync(user.Id, selectedGroups);

                        selectedUnit = selectedUnit ?? new string[] { };
                        await this.UnitManager.SetUserUnitsAsync(user.Id, selectedUnit);

                    }
                    return RedirectToAction("Index");
                }
                AddErrors(adminresult);
            }
            ViewBag.GroupsList =
                new SelectList(this.GroupManager.Groups, "Id", "Name");
            ViewBag.UnitsList =
                new SelectList(this.UnitManager.Units, "Id", "Name");
            return View(userViewModel);
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // Display a list of available Groups:
            var allGroups = this.GroupManager.Groups;
            var userGroups = await this.GroupManager.GetUserGroupsAsync(id);

            var allUnits = this.UnitManager.Units;
            var userUnits = await this.UnitManager.GetUserUnitAsync(id);

            var model = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
                //// Include the Addresss info:
                //Address = user.Address,
                //City = user.City,
                //State = user.State,
                //PostalCode = user.PostalCode,
            };
            foreach (var group in allGroups)
            {
                var listItem = new SelectListItem()
                {
                    Text = group.Name,
                    Value = group.Id,
                    Selected = userGroups.Any(g => g.Id == group.Id)
                };
                model.GroupsList.Add(listItem);
            }
            foreach (var unit in allUnits)
            {
                var listItem = new SelectListItem()
                {
                    Text = unit.UnitName,
                    Value = unit.UID,
                    Selected = userUnits.Any(g => g.UID == unit.UID)
                };
                model.UnitList.Add(listItem);
            }
            return View(model);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,FirstName,LastName")] EditUserViewModel editUser, string[] selectedGroups, params string[] selectedUnit)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                //user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                //user.Address = editUser.Address;
                //user.City = editUser.City;
                //user.State = editUser.State;
                //user.PostalCode = editUser.PostalCode;
                await UserManager.UpdateAsync(user);

                // Update the Groups:
                selectedGroups = selectedGroups ?? new string[] { };
                await this.GroupManager.SetUserGroupsAsync(user.Id, selectedGroups);

                // Update the Unit:
                selectedUnit = selectedUnit ?? new string[] { };
                await this.UnitManager.SetUserUnitsAsync(user.Id, selectedUnit);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ActionName("Closed")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ClosedConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                if (user.Closed)
                {
                    user.Closed = false;
                }
                else if (!user.Closed)
                {
                    user.Closed = true;
                    user.ClosedDateTime = DateTime.Now;
                }
                var result = await UserManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}