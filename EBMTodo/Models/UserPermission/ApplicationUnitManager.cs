using EBMTodo.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EBMTodo.Models.UserPermission
{
    public class ApplicationUnitManager
    {
        private ApplicationUnitStore _unitStore;
        private ApplicationDbContext _db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUnitManager()
        {
            _db = HttpContext.Current
                .GetOwinContext().Get<ApplicationDbContext>();
            _userManager = HttpContext.Current
                .GetOwinContext().GetUserManager<ApplicationUserManager>();
            _roleManager = HttpContext.Current
                .GetOwinContext().Get<ApplicationRoleManager>();
            _unitStore = new ApplicationUnitStore(_db);
        }


        public IQueryable<RBUnit> Units
        {
            get
            {
                return _unitStore.Groups;
            }
        }


        public async Task<IdentityResult> CreateGroupAsync(RBUnit unit)
        {
            await _unitStore.CreateAsync(unit);
            return IdentityResult.Success;
        }


        public IdentityResult CreateGroup(RBUnit unit)
        {
            _unitStore.Create(unit);
            return IdentityResult.Success;
        }


        //public IdentityResult SetGroupRoles(string groupId, params string[] roleNames)
        //{
        //    // Clear all the roles associated with this group:
        //    var thisGroup = this.FindById(groupId);
        //    thisGroup.ApplicationRoles.Clear();
        //    _db.SaveChanges();

        //    // Add the new roles passed in:
        //    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        //    foreach (var role in newRoles)
        //    {
        //        thisGroup.ApplicationRoles.Add(new ApplicationGroupRole { ApplicationGroupId = groupId, ApplicationRoleId = role.Id });
        //    }
        //    _db.SaveChanges();

        //    // Reset the roles for all affected users:
        //    foreach (var groupUser in thisGroup.ApplicationUsers)
        //    {
        //        this.RefreshUserGroupRoles(groupUser.ApplicationUserId);
        //    }
        //    return IdentityResult.Success;
        //}


        //public async Task<IdentityResult> SetGroupRolesAsync(string groupId, params string[] roleNames)
        //{
        //    // Clear all the roles associated with this group:
        //    var thisGroup = await this.FindByIdAsync(groupId);
        //    thisGroup.ApplicationRoles.Clear();
        //    await _db.SaveChangesAsync();

        //    // Add the new roles passed in:
        //    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        //    foreach (var role in newRoles)
        //    {
        //        thisGroup.ApplicationRoles.Add(new ApplicationGroupRole { ApplicationGroupId = groupId, ApplicationRoleId = role.Id });
        //    }
        //    await _db.SaveChangesAsync();

        //    // Reset the roles for all affected users:
        //    foreach (var groupUser in thisGroup.ApplicationUsers)
        //    {
        //        await this.RefreshUserGroupRolesAsync(groupUser.ApplicationUserId);
        //    }
        //    return IdentityResult.Success;
        //}


        public async Task<IdentityResult> SetUserUnitsAsync(string userId, params string[] UIDs)
        {
            // Clear current group membership:
            var currentUnits = await this.GetUserUnitAsync(userId);
            foreach (var Us in currentUnits)
            {
                _db.RBUnitUserManage.Remove(Us.RBUnitUserManage.FirstOrDefault(gr => gr.ApplicationUserId == userId));
            }
            try
            {
                await _db.SaveChangesAsync();

            }
            catch (Exception E)
            {

                throw;
            }

            // Add the user to the new groups:
            foreach (string UID in UIDs)
            {
                var newUnit = await this.FindByIdAsync(UID);
                _db.RBUnitUserManage.Add(new RBUnitUserManage { ApplicationUserId = userId, UID = UID });
            }

            try
            {
                await _db.SaveChangesAsync();

            }
            catch (Exception E)
            {

                throw;
            }

            return IdentityResult.Success;
        }


        public IdentityResult SetUserUnits(string userId, params string[] UIDs)
        {
            // Clear current group membership:
            var currentGroups = this.GetUserUnits(userId);
            foreach (var uG in currentGroups)
            {
                _db.RBUnitUserManage.Remove(uG.RBUnitUserManage.FirstOrDefault(gr => gr.ApplicationUserId == userId));
            }
            _db.SaveChanges();

            // Add the user to the new groups:
            foreach (string UID in UIDs)
            {
                var newUnit = this.FindById(UID);
                newUnit.RBUnitUserManage.Add(new RBUnitUserManage { ApplicationUserId = userId, UID = UID });
            }
            _db.SaveChanges();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteUnitAsync(string UID)
        {
            var unit = await this.FindByIdAsync(UID);
            if (unit == null)
            {
                throw new ArgumentNullException("UserUnit");
            }

            var currentUnitMembers = (await this.GetUnitUsersAsync(UID)).ToList();

            // Remove all the users:
            unit.RBUnitUserManage.Clear();

            // Remove the group itself:
            _db.RBUnit.Remove(unit);

            await _db.SaveChangesAsync();
            return IdentityResult.Success;
        }


        public IdentityResult DeleteUnit(string UID)
        {
            var unit = this.FindById(UID);
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }

            var currentUnitMembers = this.GetUnitUsers(UID).ToList();

            // Remove all the users:
            unit.RBUnitUserManage.Clear();

            // Remove the group itself:
            _db.RBUnit.Remove(unit);

            _db.SaveChanges();
            return IdentityResult.Success;
        }



        public IdentityResult ClearUserUnits(string userId)
        {
            return this.SetUserUnits(userId, new string[] { });
        }


        public async Task<IdentityResult> ClearUserUnitsAsync(string userId)
        {
            return await this.SetUserUnitsAsync(userId, new string[] { });
        }


        public async Task<IEnumerable<RBUnit>> GetUserUnitAsync(string userId)
        {
            var result = new List<RBUnit>();
            var userUnits = (from g in this.Units
                            where g.RBUnitUserManage.Any(u => u.ApplicationUserId == userId)
                            select g).ToListAsync();
            return await userUnits;
        }


        public IEnumerable<RBUnit> GetUserUnits(string userId)
        {
            var result = new List<ApplicationGroup>();
            var userGroups = (from g in this.Units
                              where g.RBUnitUserManage.Any(u => u.ApplicationUserId == userId)
                              select g).ToList();
            return userGroups;
        }

        public List<string> GetUserUID(string userId)
        {
            IEnumerable<RBUnit> R = this.GetUserUnits(userId);
            List<string> UserUnit = new List<string>();
            foreach (var H in R)
            {
                UserUnit.Add(H.UID);
            }
            return UserUnit;
        }


        public IEnumerable<ApplicationUser> GetUnitUsers(string UID)
        {
            var unit = this.FindById(UID);
            var users = new List<ApplicationUser>();
            foreach (var unitUser in unit.RBUnitUserManage)
            {
                var user = _db.Users.Find(unitUser.ApplicationUserId);
                users.Add(user);
            }
            return users;
        }


        public async Task<IEnumerable<ApplicationUser>> GetUnitUsersAsync(string unitId)
        {
            var unit = await this.FindByIdAsync(unitId);
            var users = new List<ApplicationUser>();
            foreach (var unitUser in unit.RBUnitUserManage)
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == unitUser.ApplicationUserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<RBUnit> FindByIdAsync(string id)
        {
            return await _unitStore.FindByIdAsync(id);
        }


        public RBUnit FindById(string id)
        {
            return _unitStore.FindById(id);
        }
    }
}