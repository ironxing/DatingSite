namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFriendsModelAGain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendRequest = c.Boolean(nullable: false),
                        Friends = c.Boolean(nullable: false),
                        ProfileOwnerId = c.String(maxLength: 128),
                        ProfileVisitorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfileOwnerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfileVisitorId)
                .Index(t => t.ProfileOwnerId)
                .Index(t => t.ProfileVisitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsModels", "ProfileVisitorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsModels", "ProfileOwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendsModels", new[] { "ProfileVisitorId" });
            DropIndex("dbo.FriendsModels", new[] { "ProfileOwnerId" });
            DropTable("dbo.FriendsModels");
        }
    }
}
