namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class online_department : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProjectOnline", "ApplyDepartment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoEBMProjectOnline", "ApplyDepartment");
        }
    }
}
