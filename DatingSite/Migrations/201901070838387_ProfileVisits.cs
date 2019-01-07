namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileVisits : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProfileVisits", name: "VisítorUserId", newName: "VisitorUserId");
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisítorUserId", newName: "IX_VisitorUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProfileVisits", name: "IX_VisitorUserId", newName: "IX_VisítorUserId");
            RenameColumn(table: "dbo.ProfileVisits", name: "VisitorUserId", newName: "VisítorUserId");
        }
    }
}
