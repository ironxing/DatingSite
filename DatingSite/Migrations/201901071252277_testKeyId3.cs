namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testKeyId3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfileVisits", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileVisits", "ProfileUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileVisits", "VisitorUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileVisits", new[] { "ProfileUserId" });
            DropIndex("dbo.ProfileVisits", new[] { "VisitorUserId" });
            DropIndex("dbo.ProfileVisits", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ProfileVisits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfileVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitDateTime = c.DateTime(nullable: false),
                        ProfileUserId = c.String(maxLength: 128),
                        VisitorUserId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProfileVisits", "ApplicationUser_Id");
            CreateIndex("dbo.ProfileVisits", "VisitorUserId");
            CreateIndex("dbo.ProfileVisits", "ProfileUserId");
            AddForeignKey("dbo.ProfileVisits", "VisitorUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProfileVisits", "ProfileUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProfileVisits", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
