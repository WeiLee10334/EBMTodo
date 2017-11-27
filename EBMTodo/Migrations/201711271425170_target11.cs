namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memo", "memo", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memo", "memo");
        }
    }
}
