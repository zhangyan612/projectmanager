namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBack : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tasks", "BoardId");
            RenameColumn(table: "dbo.Tasks", name: "ProjectBoard_Id", newName: "BoardId");
            RenameIndex(table: "dbo.Tasks", name: "IX_ProjectBoard_Id", newName: "IX_BoardId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tasks", name: "IX_BoardId", newName: "IX_ProjectBoard_Id");
            RenameColumn(table: "dbo.Tasks", name: "BoardId", newName: "ProjectBoard_Id");
            AddColumn("dbo.Tasks", "BoardId", c => c.Int());
        }
    }
}
