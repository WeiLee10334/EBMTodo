namespace RandomData.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EBMTodo : DbContext
    {
        public EBMTodo()
            : base("name=EBMTodo")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ApplicationGroupRoles> ApplicationGroupRoles { get; set; }
        public virtual DbSet<ApplicationGroups> ApplicationGroups { get; set; }
        public virtual DbSet<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Line_Command> Line_Command { get; set; }
        public virtual DbSet<Line_Group> Line_Group { get; set; }
        public virtual DbSet<Line_Room> Line_Room { get; set; }
        public virtual DbSet<Line_User> Line_User { get; set; }
        public virtual DbSet<Memo> Memo { get; set; }
        public virtual DbSet<RBUnit> RBUnit { get; set; }
        public virtual DbSet<RBUnitUserManage> RBUnitUserManage { get; set; }
        public virtual DbSet<TodoEBMProject> TodoEBMProject { get; set; }
        public virtual DbSet<TodoEBMProjectMember> TodoEBMProjectMember { get; set; }
        public virtual DbSet<TodoEBMProjectOnline> TodoEBMProjectOnline { get; set; }
        public virtual DbSet<TodoEBMProjectSchedule> TodoEBMProjectSchedule { get; set; }
        public virtual DbSet<TodoEBMProjectTodoList> TodoEBMProjectTodoList { get; set; }
        public virtual DbSet<TodoEBMProjectWorking> TodoEBMProjectWorking { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationGroups>()
                .HasMany(e => e.ApplicationGroupRoles)
                .WithRequired(e => e.ApplicationGroups)
                .HasForeignKey(e => e.ApplicationGroupId);

            modelBuilder.Entity<ApplicationGroups>()
                .HasMany(e => e.ApplicationUserGroups)
                .WithRequired(e => e.ApplicationGroups)
                .HasForeignKey(e => e.ApplicationGroupId);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);
        }
    }
}
