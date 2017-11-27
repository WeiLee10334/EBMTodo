namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProjectSchedule", "Title", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoEBMProjectSchedule", "Title");
        }
    }
}
