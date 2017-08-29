namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            RenameColumn(table: "dbo.Tasks", name: "Project_Id", newName: "ProjectId");
            RenameIndex(table: "dbo.Tasks", name: "IX_Project_Id", newName: "IX_ProjectId");
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Projects", "Id");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Projects", "Id");
            RenameIndex(table: "dbo.Tasks", name: "IX_ProjectId", newName: "IX_Project_Id");
            RenameColumn(table: "dbo.Tasks", name: "ProjectId", newName: "Project_Id");
            AddForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
