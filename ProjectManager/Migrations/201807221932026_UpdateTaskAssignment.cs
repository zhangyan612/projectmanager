namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaskAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskAssignments", "FullName", c => c.String());
            AddColumn("dbo.TaskAssignments", "Email", c => c.String());
            AddColumn("dbo.TaskAssignments", "PlannedHours", c => c.Double(nullable: false));
            AddColumn("dbo.TaskAssignments", "ActualHours", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskAssignments", "ActualHours");
            DropColumn("dbo.TaskAssignments", "PlannedHours");
            DropColumn("dbo.TaskAssignments", "Email");
            DropColumn("dbo.TaskAssignments", "FullName");
        }
    }
}
