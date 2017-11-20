namespace EBMTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class target4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Line_Command", "IssueNo", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Line_Command", "ReplyToken", c => c.String(maxLength: 128));
            AddColumn("dbo.Line_Command", "LineType", c => c.String(maxLength: 50));
            CreateIndex("dbo.Line_Command", "IssueNo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Line_Command", new[] { "IssueNo" });
            DropColumn("dbo.Line_Command", "LineType");
            DropColumn("dbo.Line_Command", "ReplyToken");
            DropColumn("dbo.Line_Command", "IssueNo");
        }
    }
}
