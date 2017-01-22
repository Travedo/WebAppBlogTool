namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedKey : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
