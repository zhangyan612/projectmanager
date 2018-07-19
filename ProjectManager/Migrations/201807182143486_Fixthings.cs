namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixthings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "Project_Id", "dbo.Projects");
            DropIndex("dbo.UserProfiles", new[] { "Project_Id" });
            CreateTable(
                "dbo.Collaborators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            DropColumn("dbo.UserProfiles", "Project_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Project_Id", c => c.Guid());
            DropForeignKey("dbo.Collaborators", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Collaborators", new[] { "ProjectId" });
            DropTable("dbo.Collaborators");
            CreateIndex("dbo.UserProfiles", "Project_Id");
            AddForeignKey("dbo.UserProfiles", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
