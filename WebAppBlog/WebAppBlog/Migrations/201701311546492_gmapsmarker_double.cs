namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gmapsmarker_double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GMapsMarkerModels", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.GMapsMarkerModels", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GMapsMarkerModels", "Longitude", c => c.String());
            AlterColumn("dbo.GMapsMarkerModels", "Latitude", c => c.String());
        }
    }
}
