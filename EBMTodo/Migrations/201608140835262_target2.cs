namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProject", "IsHode", c => c.Boolean(nullable: false));
            AddColumn("dbo.TodoEBMProjectWorking", "workingType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoEBMProjectWorking", "workingType");
            DropColumn("dbo.TodoEBMProject", "IsHode");
        }
    }
}
