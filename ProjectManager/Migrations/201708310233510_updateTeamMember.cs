namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTeamMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMembers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamMembers", new[] { "Team_Id" });
            RenameColumn(table: "dbo.TeamMembers", name: "Team_Id", newName: "TeamId");
            AddColumn("dbo.TeamMembers", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.TeamMembers", "TeamId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TeamMembers", "TeamId");
            AddForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            AlterColumn("dbo.TeamMembers", "TeamId", c => c.Guid());
            DropColumn("dbo.TeamMembers", "UserId");
            RenameColumn(table: "dbo.TeamMembers", name: "TeamId", newName: "Team_Id");
            CreateIndex("dbo.TeamMembers", "Team_Id");
            AddForeignKey("dbo.TeamMembers", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
