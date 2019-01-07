namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autokeys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProfileVisits", name: "ProfileUserId", newName: "ProfileUser_Id");
            RenameColumn(table: "dbo.ProfileVisits", name: "VisitorUserId", newName: "VisitorUser_Id");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_ProfileUserId", newName: "IX_ProfileUser_Id");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisitorUserId", newName: "IX_VisitorUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisitorUser_Id", newName: "IX_VisitorUserId");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_ProfileUser_Id", newName: "IX_ProfileUserId");
            RenameColumn(table: "dbo.ProfileVisits", name: "VisitorUser_Id", newName: "VisitorUserId");
            RenameColumn(table: "dbo.ProfileVisits", name: "ProfileUser_Id", newName: "ProfileUserId");
        }
    }
}
