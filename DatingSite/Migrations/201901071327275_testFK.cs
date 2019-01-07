namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProfileVisits", name: "ProfileUser_Id", newName: "ProfileUserId");
            RenameColumn(table: "dbo.ProfileVisits", name: "VisitorUser_Id", newName: "VisitorUserId");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_ProfileUser_Id", newName: "IX_ProfileUserId");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisitorUser_Id", newName: "IX_VisitorUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisitorUserId", newName: "IX_VisitorUser_Id");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_ProfileUserId", newName: "IX_ProfileUser_Id");
            RenameColumn(table: "dbo.ProfileVisits", name: "VisitorUserId", newName: "VisitorUser_Id");
            RenameColumn(table: "dbo.ProfileVisits", name: "ProfileUserId", newName: "ProfileUser_Id");
        }
    }
}
