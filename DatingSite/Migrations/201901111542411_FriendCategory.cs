namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryOwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CategoryOwnerId)
                .Index(t => t.CategoryOwnerId);
            
            AddColumn("dbo.FriendsModels", "ProfileOwnerCategoryId", c => c.Int());
            AddColumn("dbo.FriendsModels", "ProfileVisitorCategoryId", c => c.Int());
            CreateIndex("dbo.FriendsModels", "ProfileOwnerCategoryId");
            CreateIndex("dbo.FriendsModels", "ProfileVisitorCategoryId");
            AddForeignKey("dbo.FriendsModels", "ProfileOwnerCategoryId", "dbo.FriendCategories", "Id");
            AddForeignKey("dbo.FriendsModels", "ProfileVisitorCategoryId", "dbo.FriendCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsModels", "ProfileVisitorCategoryId", "dbo.FriendCategories");
            DropForeignKey("dbo.FriendsModels", "ProfileOwnerCategoryId", "dbo.FriendCategories");
            DropForeignKey("dbo.FriendCategories", "CategoryOwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendCategories", new[] { "CategoryOwnerId" });
            DropIndex("dbo.FriendsModels", new[] { "ProfileVisitorCategoryId" });
            DropIndex("dbo.FriendsModels", new[] { "ProfileOwnerCategoryId" });
            DropColumn("dbo.FriendsModels", "ProfileVisitorCategoryId");
            DropColumn("dbo.FriendsModels", "ProfileOwnerCategoryId");
            DropTable("dbo.FriendCategories");
        }
    }
}
