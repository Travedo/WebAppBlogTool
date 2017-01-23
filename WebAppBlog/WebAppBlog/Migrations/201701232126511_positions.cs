namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class positions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryModels", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.ImageModels", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.TextModels", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextModels", "Position");
            DropColumn("dbo.ImageModels", "Position");
            DropColumn("dbo.GalleryModels", "Position");
        }
    }
}
