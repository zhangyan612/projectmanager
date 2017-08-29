namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        OwnerId = c.String(),
                        Desc = c.String(),
                        Public = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "Project_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tasks", "Project_Id");
            AddForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_Id" });
            DropColumn("dbo.Tasks", "Project_Id");
            DropTable("dbo.Projects");
        }
    }
}
