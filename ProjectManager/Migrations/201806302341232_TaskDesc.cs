namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskDesc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "DescriptionId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "DescriptionId");
            DropTable("dbo.TaskDescriptions");
        }
    }
}
