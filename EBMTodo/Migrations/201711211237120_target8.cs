namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoEBMProjectSchedule",
                c => new
                    {
                        PSID = c.Guid(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ScheduleDateTime = c.DateTime(nullable: false),
                        Target = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        WokingHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        scheduleType = c.Int(nullable: false),
                        FinishDateTime = c.DateTime(nullable: false),
                        LineUID = c.String(maxLength: 128),
                        Id = c.String(maxLength: 128),
                        PID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PSID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.TodoEBMProject", t => t.PID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.PID);
            
            CreateTable(
                "dbo.Memo",
                c => new
                    {
                        MID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        Tag = c.String(maxLength: 100),
                        Content = c.String(maxLength: 500),
                        memoType = c.Int(nullable: false),
                        LineUID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MID);
            
            AlterColumn("dbo.TodoEBMProjectWorking", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoEBMProjectSchedule", "PID", "dbo.TodoEBMProject");
            DropForeignKey("dbo.TodoEBMProjectSchedule", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.TodoEBMProjectSchedule", new[] { "PID" });
            DropIndex("dbo.TodoEBMProjectSchedule", new[] { "Id" });
            AlterColumn("dbo.TodoEBMProjectWorking", "Description", c => c.String(maxLength: 100));
            DropTable("dbo.Memo");
            DropTable("dbo.TodoEBMProjectSchedule");
        }
    }
}
