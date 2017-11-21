namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Line_User",
                c => new
                    {
                        UID = c.String(nullable: false, maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.UID);
            
            AddColumn("dbo.Line_Command", "assignUserName", c => c.String(maxLength: 100));
            DropColumn("dbo.Line_Group", "CommandType");
            DropColumn("dbo.Line_Group", "CommandStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Line_Group", "CommandStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Line_Group", "CommandType", c => c.Int(nullable: false));
            DropColumn("dbo.Line_Command", "assignUserName");
            DropTable("dbo.Line_User");
        }
    }
}
