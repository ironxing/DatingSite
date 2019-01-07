namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testId4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitDateTime = c.DateTime(nullable: false),
                        ProfileUserId = c.String(maxLength: 128),
                        VisitorUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfileUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.VisitorUserId)
                .Index(t => t.ProfileUserId)
                .Index(t => t.VisitorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileVisits", "VisitorUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileVisits", "ProfileUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileVisits", new[] { "VisitorUserId" });
            DropIndex("dbo.ProfileVisits", new[] { "ProfileUserId" });
            DropTable("dbo.ProfileVisits");
        }
    }
}
