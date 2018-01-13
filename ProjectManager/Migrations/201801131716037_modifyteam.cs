namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyteam : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Projects", new[] { "Team_Id" });
            DropColumn("dbo.Projects", "TeamId");
            RenameColumn(table: "dbo.Projects", name: "Team_Id", newName: "TeamId");
            AlterColumn("dbo.Projects", "TeamId", c => c.Guid());
            CreateIndex("dbo.Projects", "TeamId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "TeamId" });
            AlterColumn("dbo.Projects", "TeamId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Projects", name: "TeamId", newName: "Team_Id");
            AddColumn("dbo.Projects", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "Team_Id");
        }
    }
}
