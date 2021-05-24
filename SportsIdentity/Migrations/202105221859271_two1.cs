namespace SportsIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "FootBallTeam_ID", "dbo.FootBallTeams");
            DropIndex("dbo.Players", new[] { "FootBallTeam_ID" });
            DropColumn("dbo.Players", "IDTeam");
            RenameColumn(table: "dbo.Players", name: "FootBallTeam_ID", newName: "IDTeam");
            AlterColumn("dbo.Players", "IDTeam", c => c.Int(nullable: false));
            CreateIndex("dbo.Players", "IDTeam");
            AddForeignKey("dbo.Players", "IDTeam", "dbo.FootBallTeams", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "IDTeam", "dbo.FootBallTeams");
            DropIndex("dbo.Players", new[] { "IDTeam" });
            AlterColumn("dbo.Players", "IDTeam", c => c.Int());
            RenameColumn(table: "dbo.Players", name: "IDTeam", newName: "FootBallTeam_ID");
            AddColumn("dbo.Players", "IDTeam", c => c.Int(nullable: false));
            CreateIndex("dbo.Players", "FootBallTeam_ID");
            AddForeignKey("dbo.Players", "FootBallTeam_ID", "dbo.FootBallTeams", "ID");
        }
    }
}
