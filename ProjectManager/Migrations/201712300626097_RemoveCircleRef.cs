namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCircleRef : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "BoardId", "dbo.ProjectBoards");
            DropIndex("dbo.Tasks", new[] { "BoardId" });
            AddColumn("dbo.Tasks", "ProjectBoard_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "ProjectBoard_Id");
            AddForeignKey("dbo.Tasks", "ProjectBoard_Id", "dbo.ProjectBoards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectBoard_Id", "dbo.ProjectBoards");
            DropIndex("dbo.Tasks", new[] { "ProjectBoard_Id" });
            DropColumn("dbo.Tasks", "ProjectBoard_Id");
            CreateIndex("dbo.Tasks", "BoardId");
            AddForeignKey("dbo.Tasks", "BoardId", "dbo.ProjectBoards", "Id");
        }
    }
}
