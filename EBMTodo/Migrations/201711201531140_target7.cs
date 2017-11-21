namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProjectWorking", "Target", c => c.String(maxLength: 100));
            AddColumn("dbo.TodoEBMProjectWorking", "RecordDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TodoEBMProjectWorking", "LineUID", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoEBMProjectWorking", "LineUID");
            DropColumn("dbo.TodoEBMProjectWorking", "RecordDateTime");
            DropColumn("dbo.TodoEBMProjectWorking", "Target");
        }
    }
}
