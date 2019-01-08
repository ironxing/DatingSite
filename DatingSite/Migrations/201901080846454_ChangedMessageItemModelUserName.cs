namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMessageItemModelUserName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MessageItems", name: "ProfileUserId", newName: "MessageReceiverId");
            RenameIndex(table: "dbo.MessageItems", name: "IX_ProfileUserId", newName: "IX_MessageReceiverId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MessageItems", name: "IX_MessageReceiverId", newName: "IX_ProfileUserId");
            RenameColumn(table: "dbo.MessageItems", name: "MessageReceiverId", newName: "ProfileUserId");
        }
    }
}
