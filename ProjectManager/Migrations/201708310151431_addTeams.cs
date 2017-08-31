namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "Team_Id", c => c.Guid());
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Guid());
            AlterColumn("dbo.Projects", "CreatedDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Projects", "Team_Id");
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
            AddForeignKey("dbo.Projects", "Team_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            DropIndex("dbo.Projects", new[] { "Team_Id" });
            AlterColumn("dbo.Projects", "CreatedDate", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "Team_Id");
            DropColumn("dbo.Projects", "Team_Id");
            DropTable("dbo.Teams");
        }
    }
}
