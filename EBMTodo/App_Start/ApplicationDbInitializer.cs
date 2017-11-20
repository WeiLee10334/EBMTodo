using EBMTodo.Models.UserPermission;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using EBMTodo.Models.Base;
using EBMTodo.Models.Base.Enum;
namespace EBMTodo.Models
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);

            ApplicationDbContext DefaultCom = new ApplicationDbContext();

            //Data
            //========================================
            DefaultCom.RBUnit.AddOrUpdate(
                U => U.UID,
                new RBUnit { UID = "EBMTechOrg001",UnitName="EBM",UnitLevel=UnitLevel.Organization}
                );

            DefaultCom.SaveChanges();

            base.Seed(context);
        }

        //User Init
        //================================
        string[] _initialUserName =
           new string[] { "admin" };
        const string _initialUserpassword = "Pa55w0rd";
        string adminId = "";


        private void InitializeIdentityForEF(ApplicationDbContext db)
        {
            //Group Init
            //================================
            List<string> _initialGroupNames = new List<string>();
            foreach (var item in Enum.GetValues(typeof(UserGroup)))
            {
                _initialGroupNames.Add(item.ToString());
            }

            //Role Init
            //================================
            List<string> _AdministratorRoleNames = new List<string>();
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                _AdministratorRoleNames.Add(item.ToString());
            }
           
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            foreach (var initUser in _initialUserName)
            {
                var user = userManager.FindByName(initUser);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = initUser, Email = initUser + "@ebmtech.com", EmailConfirmed = true, FirstName = "EBM", LastName = initUser };
                    var result = userManager.Create(user, _initialUserpassword);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                    //userManager.AddClaim(user.Id, new Claim("FullName", user.FirstName + " " + user.LastName));
                }
                //TODO 發布時不重建DB
                ApplicationDbInitializer ADI = new ApplicationDbInitializer();
                if (initUser == "admin")
                { adminId = user.Id; }
            }
            var groupManager = new ApplicationGroupManager();
            List<string> groupId = new List<string>();
            foreach (var G in _initialGroupNames)
            {
                var newGroup = new ApplicationGroup(G, G);
                groupManager.CreateGroup(newGroup);
                groupId.Add(newGroup.Id);
                if (G == "SystemAdmin")
                {
                    //Create Role Admin if it does not exist
                    foreach (var R in _AdministratorRoleNames)
                    {
                        var role = roleManager.FindByName(R);
                        if (role == null)
                        {
                            role = new ApplicationRole(R);
                            var roleresult = roleManager.Create(role);
                        }
                    }
                    groupManager.SetGroupRoles(newGroup.Id, _AdministratorRoleNames.ToArray());
                    groupManager.SetUserGroups(adminId, newGroup.Id);
                }
            }
            groupManager.SetUserGroups(adminId, groupId.ToArray());

        }
    }
}