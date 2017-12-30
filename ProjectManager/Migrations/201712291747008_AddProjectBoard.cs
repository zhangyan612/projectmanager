namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectBoard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectBoards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Tasks", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tasks", "BoardId", c => c.Int());
            CreateIndex("dbo.Tasks", "BoardId");
            AddForeignKey("dbo.Tasks", "BoardId", "dbo.ProjectBoards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectBoards", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "BoardId", "dbo.ProjectBoards");
            DropIndex("dbo.Tasks", new[] { "BoardId" });
            DropIndex("dbo.ProjectBoards", new[] { "ProjectId" });
            DropColumn("dbo.Tasks", "BoardId");
            DropColumn("dbo.Tasks", "Active");
            DropTable("dbo.ProjectBoards");
        }
    }
}
