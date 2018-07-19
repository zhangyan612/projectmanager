namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskAssignmentStr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskAssignments", "UserProfileId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskAssignments", "UserProfileId", c => c.Int(nullable: false));
        }
    }
}
