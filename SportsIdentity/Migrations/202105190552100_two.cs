namespace SportsIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Players", new[] { "UserID" });
            DropColumn("dbo.Players", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Players", "UserID");
            AddForeignKey("dbo.Players", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
