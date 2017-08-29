namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Projects", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Tasks", "ProjectId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Projects", "Id");
            CreateIndex("dbo.Tasks", "ProjectId");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Tasks", "ProjectId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Projects", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Projects", "Id");
            CreateIndex("dbo.Tasks", "ProjectId");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id");
        }
    }
}
