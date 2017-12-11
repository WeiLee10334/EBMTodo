namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target12_AddOnlineTblUpdateTodoCol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoEBMProjectOnline",
                c => new
                    {
                        POID = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ApplyDateTime = c.DateTime(nullable: false),
                        ApplyName = c.String(maxLength: 100),
                        title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        CompleteRate = c.Int(nullable: false),
                        OnlineCategories = c.Int(nullable: false),
                        Memo = c.String(maxLength: 500),
                        PMID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.POID)
                .ForeignKey("dbo.TodoEBMProjectMember", t => t.PMID, cascadeDelete: true)
                .Index(t => t.PMID);
            
            AddColumn("dbo.TodoEBMProjectTodoList", "ApplyDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TodoEBMProjectTodoList", "ApplyName", c => c.String(maxLength: 100));
            AddColumn("dbo.TodoEBMProjectTodoList", "Tag", c => c.String(maxLength: 200));
            AddColumn("dbo.TodoEBMProjectTodoList", "Memo", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoEBMProjectOnline", "PMID", "dbo.TodoEBMProjectMember");
            DropIndex("dbo.TodoEBMProjectOnline", new[] { "PMID" });
            DropColumn("dbo.TodoEBMProjectTodoList", "Memo");
            DropColumn("dbo.TodoEBMProjectTodoList", "Tag");
            DropColumn("dbo.TodoEBMProjectTodoList", "ApplyName");
            DropColumn("dbo.TodoEBMProjectTodoList", "ApplyDateTime");
            DropTable("dbo.TodoEBMProjectOnline");
        }
    }
}
