namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "LookingForGender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LookingForGender", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
    }
}
