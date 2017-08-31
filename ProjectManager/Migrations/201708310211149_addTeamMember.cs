namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeamMember : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Team_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.AspNetUsers", "Team_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Guid());
            DropForeignKey("dbo.TeamMembers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TeamMembers", new[] { "User_Id" });
            DropIndex("dbo.TeamMembers", new[] { "Team_Id" });
            DropTable("dbo.TeamMembers");
            CreateIndex("dbo.AspNetUsers", "Team_Id");
        }
    }
}
