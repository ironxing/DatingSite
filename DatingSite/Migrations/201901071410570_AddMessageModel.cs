namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        messageTime = c.DateTime(nullable: false),
                        ProfileUserId = c.String(maxLength: 128),
                        MessageSenderId = c.String(maxLength: 128),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.MessageSenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfileUserId)
                .Index(t => t.ProfileUserId)
                .Index(t => t.MessageSenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageItems", "ProfileUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageItems", "MessageSenderId", "dbo.AspNetUsers");
            DropIndex("dbo.MessageItems", new[] { "MessageSenderId" });
            DropIndex("dbo.MessageItems", new[] { "ProfileUserId" });
            DropTable("dbo.MessageItems");
        }
    }
}
