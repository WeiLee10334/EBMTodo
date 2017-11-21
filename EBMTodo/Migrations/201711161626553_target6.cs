namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Line_Room",
                c => new
                    {
                        RoomID = c.String(nullable: false, maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Line_Room");
        }
    }
}
