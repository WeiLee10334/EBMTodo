using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EBMTodo.Models.Base;
using EBMTodo.Models.Todo;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBMTodo.Models
{
    // You will not likely need to customize there, but it is necessary/easier to create our own 
    // project-specific implementations, so here they are:
    public class ApplicationUserLogin : IdentityUserLogin<string> { }
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserRole : IdentityUserRole<string> { }

    // 您可以在 ApplicationUser 類別新增更多屬性，為使用者新增設定檔資料，請造訪 http://go.microsoft.com/fwlink/?LinkID=317594 以深入了解。
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            //HospitalManage = new HashSet<HospitalManage>();
            this.Id = Guid.NewGuid().ToString();
            // Add any custom User properties/code here
            IsADUser = false;
            EBMProjectMember = new HashSet<EBMProjectMember>();
            EBMProjectWorking = new HashSet<EBMProjectWorking>();
            EBMProjectSchedule = new HashSet<EBMProjectSchedule>();
        }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        [Index]
        [StringLength(256)]
        public string LineID { get; set; }
        public bool Closed { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public bool IsADUser { get; set; }
        public virtual ICollection<EBMProjectMember> EBMProjectMember { get; set; }
        public virtual ICollection<EBMProjectWorking> EBMProjectWorking { get; set; }
        public virtual ICollection<EBMProjectSchedule> EBMProjectSchedule { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
    }
    // Must be expressed in terms of our custom UserRole:
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string Description)
            : this()
        {
            this.Name = name;
            this.Description = Description;
        }
        public string Description { get; set; }
        // Add any custom Role properties/code here
    }
    //初始DB
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("EBMTodoEntity")
        {
        }
        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            //TODO 發布時不重建DB
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //建置DB Binding
        //=====================================
        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public virtual DbSet<RBUnit> RBUnit { get; set; }
        public virtual DbSet<RBUnitUserManage> RBUnitUserManage { get; set; }
        public virtual DbSet<EBMProject> EBMProject { get; set; }

        public virtual DbSet<EBMProjectMember> EBMProjectMember { get; set; }
        public virtual DbSet<EBMProjectTodoList> EBMProjectTodoList { get; set; }
        public virtual DbSet<EBMProjectWorking> EBMProjectWorking { get; set; }
        public virtual DbSet<LineCommand> LineCommand { get; set; }
        public virtual DbSet<LineGroup> LineGroup { get; set; }
        public virtual DbSet<LineUser> LineUser { get; set; }
        public virtual DbSet<LineRoom> LineRoom { get; set; }
        public virtual DbSet<Memo> Memo { get; set; }
        public virtual DbSet<EBMProjectSchedule> EBMProjectSchedule { get; set; }
        public virtual DbSet<EBMProjectOnline> EBMProjectOnline { get; set; }

        //=====================================
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             // Make sure to call the base method first:
             base.OnModelCreating(modelBuilder);

             // Map Users to Groups:
             modelBuilder.Entity<ApplicationGroup>()
                 .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                 .WithRequired()
                 .HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);

             modelBuilder.Entity<ApplicationUserGroup>()
                 .HasKey((ApplicationUserGroup r) =>
                     new
                     {
                         ApplicationUserId = r.ApplicationUserId,
                         ApplicationGroupId = r.ApplicationGroupId
                     }).ToTable("ApplicationUserGroups");

             // Map Roles to Groups:
             modelBuilder.Entity<ApplicationGroup>()
                 .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                 .WithRequired()
                 .HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);

             modelBuilder.Entity<ApplicationGroupRole>()
                .HasKey((ApplicationGroupRole gr) =>
                 new
                 {
                     ApplicationRoleId = gr.ApplicationRoleId,
                     ApplicationGroupId = gr.ApplicationGroupId
                 }).ToTable("ApplicationGroupRoles");


            //modelBuilder.Entity<RBUnit>()
            // .HasMany<RBUnitUserManage>((RBUnit g) => g.RBUnitUserManage)
            // .WithRequired()
            // .HasForeignKey<string>((RBUnitUserManage ag) => ag.ApplicationUserId);

            modelBuilder.Entity<RBUnitUserManage>()
                .HasKey((RBUnitUserManage gr) =>
                 new
                 {
                     UID = gr.UID,
                     ApplicationUserId = gr.ApplicationUserId
                 }).ToTable("RBUnitUserManage");



        }

    }

    // Most likely won't need to customize these either, but they were needed because we implemented
    // custom versions of all the other types:
    public class ApplicationUserStore
        : UserStore<ApplicationUser, ApplicationRole, string,
            ApplicationUserLogin, ApplicationUserRole,
            ApplicationUserClaim>, IUserStore<ApplicationUser, string>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationRoleStore
    : RoleStore<ApplicationRole, string, ApplicationUserRole>,
    IQueryableRoleStore<ApplicationRole, string>,
    IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
        }

        public ApplicationGroup(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    }
    public class ApplicationUserGroup
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationGroupId { get; set; }
    }

    public class ApplicationGroupRole
    {
        public string ApplicationGroupId { get; set; }
        public string ApplicationRoleId { get; set; }
    }
}