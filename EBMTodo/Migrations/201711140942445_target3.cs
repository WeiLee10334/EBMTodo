namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Line_Command",
                c => new
                    {
                        LCID = c.Guid(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        UserName = c.String(maxLength: 128),
                        GroupID = c.String(maxLength: 128),
                        RoomId = c.String(maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                        Command = c.String(maxLength: 5),
                        Message = c.String(maxLength: 2000),
                        CommandType = c.Int(nullable: false),
                        CommandStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LCID);
            
            CreateTable(
                "dbo.Line_Group",
                c => new
                    {
                        GroupID = c.String(nullable: false, maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 200),
                        CommandType = c.Int(nullable: false),
                        CommandStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            AddColumn("dbo.AspNetUsers", "LineID", c => c.String(maxLength: 256));
            CreateIndex("dbo.AspNetUsers", "LineID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "LineID" });
            DropColumn("dbo.AspNetUsers", "LineID");
            DropTable("dbo.Line_Group");
            DropTable("dbo.Line_Command");
        }
    }
}
