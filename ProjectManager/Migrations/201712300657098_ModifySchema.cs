namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectBoards", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectBoards", new[] { "ProjectId" });
            RenameColumn(table: "dbo.ProjectBoards", name: "ProjectId", newName: "Project_Id");
            AlterColumn("dbo.ProjectBoards", "Project_Id", c => c.Guid());
            CreateIndex("dbo.ProjectBoards", "Project_Id");
            AddForeignKey("dbo.ProjectBoards", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectBoards", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectBoards", new[] { "Project_Id" });
            AlterColumn("dbo.ProjectBoards", "Project_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.ProjectBoards", name: "Project_Id", newName: "ProjectId");
            CreateIndex("dbo.ProjectBoards", "ProjectId");
            AddForeignKey("dbo.ProjectBoards", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
