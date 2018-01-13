namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateteam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TeamProjects", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamProjects", new[] { "TeamId" });
            DropIndex("dbo.TeamProjects", new[] { "ProjectId" });
            CreateTable(
                "dbo.Collaborators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeamId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Email = c.String(),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "isTeamProject", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "TeamId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Team_Id", c => c.Guid());
            CreateIndex("dbo.Projects", "Team_Id");
            AddForeignKey("dbo.Projects", "Team_Id", "dbo.Teams", "Id");
            DropTable("dbo.TeamProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Projects", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Collaborators", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Collaborators", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "Team_Id" });
            DropColumn("dbo.Projects", "Team_Id");
            DropColumn("dbo.Projects", "TeamId");
            DropColumn("dbo.Projects", "isTeamProject");
            DropTable("dbo.Collaborators");
            CreateIndex("dbo.TeamProjects", "ProjectId");
            CreateIndex("dbo.TeamProjects", "TeamId");
            AddForeignKey("dbo.TeamProjects", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
