namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProjectWorking", "ProgressingFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.TodoEBMProjectSchedule", "ProgressingFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Memo", "ProgressingFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memo", "ProgressingFlag");
            DropColumn("dbo.TodoEBMProjectSchedule", "ProgressingFlag");
            DropColumn("dbo.TodoEBMProjectWorking", "ProgressingFlag");
        }
    }
}
