namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBoard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectBoards", "cssClass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectBoards", "cssClass");
        }
    }
}
