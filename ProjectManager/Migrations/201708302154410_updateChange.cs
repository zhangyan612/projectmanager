namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        DateEdited = c.DateTime(nullable: false),
                        Email = c.String(),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Boolean(),
                        Address = c.String(),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        ZipCode = c.Double(),
                        ContactNo = c.Double(),
                        UserId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfiles");
        }
    }
}
