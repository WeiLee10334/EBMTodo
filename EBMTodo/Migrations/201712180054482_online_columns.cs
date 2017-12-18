namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class online_columns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoEBMProjectOnline", "HandleDateTime", c => c.DateTime());
            AddColumn("dbo.TodoEBMProjectOnline", "ResolveDateTime", c => c.DateTime());
            AddColumn("dbo.TodoEBMProjectOnline", "ResponseName", c => c.String(maxLength: 100));
            AddColumn("dbo.TodoEBMProjectOnline", "HandleName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoEBMProjectOnline", "HandleName");
            DropColumn("dbo.TodoEBMProjectOnline", "ResponseName");
            DropColumn("dbo.TodoEBMProjectOnline", "ResolveDateTime");
            DropColumn("dbo.TodoEBMProjectOnline", "HandleDateTime");
        }
    }
}
