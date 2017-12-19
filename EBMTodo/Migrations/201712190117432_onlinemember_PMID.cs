namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onlinemember_PMID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TodoEBMProjectOnline", "PMID", "dbo.TodoEBMProjectMember");
            DropIndex("dbo.TodoEBMProjectOnline", new[] { "PMID" });
            AlterColumn("dbo.TodoEBMProjectOnline", "PMID", c => c.Guid());
            CreateIndex("dbo.TodoEBMProjectOnline", "PMID");
            AddForeignKey("dbo.TodoEBMProjectOnline", "PMID", "dbo.TodoEBMProjectMember", "PMID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoEBMProjectOnline", "PMID", "dbo.TodoEBMProjectMember");
            DropIndex("dbo.TodoEBMProjectOnline", new[] { "PMID" });
            AlterColumn("dbo.TodoEBMProjectOnline", "PMID", c => c.Guid(nullable: false));
            CreateIndex("dbo.TodoEBMProjectOnline", "PMID");
            AddForeignKey("dbo.TodoEBMProjectOnline", "PMID", "dbo.TodoEBMProjectMember", "PMID", cascadeDelete: true);
        }
    }
}
