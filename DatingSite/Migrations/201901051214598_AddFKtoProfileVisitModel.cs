
namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKtoProfileVisitModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProfileVisits", name: "UserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.ProfileVisits", "ProfileUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ProfileVisits", "VisítorUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProfileVisits", "ProfileUserId");
            CreateIndex("dbo.ProfileVisits", "VisítorUserId");
            AddForeignKey("dbo.ProfileVisits", "ProfileUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProfileVisits", "VisítorUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileVisits", "VisítorUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileVisits", "ProfileUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileVisits", new[] { "VisítorUserId" });
            DropIndex("dbo.ProfileVisits", new[] { "ProfileUserId" });
            DropColumn("dbo.ProfileVisits", "VisítorUserId");
            DropColumn("dbo.ProfileVisits", "ProfileUserId");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.ProfileVisits", name: "ApplicationUser_Id", newName: "UserId");
        }
    }
}
