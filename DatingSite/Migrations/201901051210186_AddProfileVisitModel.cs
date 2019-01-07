namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileVisitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileVisits", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileVisits", new[] { "UserId" });
            DropTable("dbo.ProfileVisits");
        }
    }
}
