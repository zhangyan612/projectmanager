namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateteam1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "isTeamProject");
            DropColumn("dbo.Collaborators", "Name");
            DropColumn("dbo.Collaborators", "TeamId");
            DropColumn("dbo.Collaborators", "Email");
            DropColumn("dbo.TeamMembers", "Name");
            DropColumn("dbo.TeamMembers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamMembers", "Email", c => c.String());
            AddColumn("dbo.TeamMembers", "Name", c => c.String());
            AddColumn("dbo.Collaborators", "Email", c => c.String());
            AddColumn("dbo.Collaborators", "TeamId", c => c.Guid(nullable: false));
            AddColumn("dbo.Collaborators", "Name", c => c.String());
            AddColumn("dbo.Projects", "isTeamProject", c => c.Boolean(nullable: false));
        }
    }
}
