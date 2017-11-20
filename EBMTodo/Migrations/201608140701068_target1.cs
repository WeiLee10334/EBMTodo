namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoEBMProjectMember",
                c => new
                    {
                        PMID = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        title = c.String(maxLength: 100),
                        Id = c.String(maxLength: 128),
                        PID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PMID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.TodoEBMProject", t => t.PID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.TodoEBMProject",
                c => new
                    {
                        PID = c.Guid(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 100),
                        CreateDateTime = c.DateTime(nullable: false),
                        ProjectNo = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PID);
            
            CreateTable(
                "dbo.TodoEBMProjectTodoList",
                c => new
                    {
                        PTLID = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        CompleteRate = c.Int(nullable: false),
                        PMID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PTLID)
                .ForeignKey("dbo.TodoEBMProjectMember", t => t.PMID, cascadeDelete: true)
                .Index(t => t.PMID);
            
            CreateTable(
                "dbo.TodoEBMProjectWorking",
                c => new
                    {
                        PWID = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 100),
                        WokingHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Id = c.String(nullable: false, maxLength: 128),
                        PID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PWID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.TodoEBMProject", t => t.PID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.PID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoEBMProjectWorking", "PID", "dbo.TodoEBMProject");
            DropForeignKey("dbo.TodoEBMProjectWorking", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TodoEBMProjectTodoList", "PMID", "dbo.TodoEBMProjectMember");
            DropForeignKey("dbo.TodoEBMProjectMember", "PID", "dbo.TodoEBMProject");
            DropForeignKey("dbo.TodoEBMProjectMember", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.TodoEBMProjectWorking", new[] { "PID" });
            DropIndex("dbo.TodoEBMProjectWorking", new[] { "Id" });
            DropIndex("dbo.TodoEBMProjectTodoList", new[] { "PMID" });
            DropIndex("dbo.TodoEBMProjectMember", new[] { "PID" });
            DropIndex("dbo.TodoEBMProjectMember", new[] { "Id" });
            DropTable("dbo.TodoEBMProjectWorking");
            DropTable("dbo.TodoEBMProjectTodoList");
            DropTable("dbo.TodoEBMProject");
            DropTable("dbo.TodoEBMProjectMember");
        }
    }
}
