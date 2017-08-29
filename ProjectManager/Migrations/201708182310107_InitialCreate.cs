namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.Tasks");
            //DropTable("dbo.Links");

            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 1),
                        SourceTaskId = c.Int(nullable: false),
                        TargetTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 255),
                        StartDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Progress = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SortOrder = c.Int(nullable: false),
                        Type = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tasks");
            DropTable("dbo.Links");
        }
    }
}
