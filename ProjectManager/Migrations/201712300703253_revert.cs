namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectBoards", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectBoards", new[] { "Project_Id" });
            RenameColumn(table: "dbo.ProjectBoards", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.ProjectBoards", "ProjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProjectBoards", "ProjectId");
            AddForeignKey("dbo.ProjectBoards", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectBoards", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectBoards", new[] { "ProjectId" });
            AlterColumn("dbo.ProjectBoards", "ProjectId", c => c.Guid());
            RenameColumn(table: "dbo.ProjectBoards", name: "ProjectId", newName: "Project_Id");
            CreateIndex("dbo.ProjectBoards", "Project_Id");
            AddForeignKey("dbo.ProjectBoards", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
