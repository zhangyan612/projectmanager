namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeamProjects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMembers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Projects", new[] { "Team_Id" });
            DropIndex("dbo.TeamMembers", new[] { "User_Id" });
            CreateTable(
                "dbo.TeamProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.ProjectId);
            
            DropColumn("dbo.Projects", "Team_Id");
            DropColumn("dbo.TeamMembers", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamMembers", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Projects", "Team_Id", c => c.Guid());
            DropForeignKey("dbo.TeamProjects", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamProjects", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TeamProjects", new[] { "ProjectId" });
            DropIndex("dbo.TeamProjects", new[] { "TeamId" });
            DropTable("dbo.TeamProjects");
            CreateIndex("dbo.TeamMembers", "User_Id");
            CreateIndex("dbo.Projects", "Team_Id");
            AddForeignKey("dbo.Projects", "Team_Id", "dbo.Teams", "Id");
            AddForeignKey("dbo.TeamMembers", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
