namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gmapsmarker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GMapsMarkerModels",
                c => new
                    {
                        GMapsMarkerModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.GMapsMarkerModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId)
                .Index(t => t.BlogData_BlogDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GMapsMarkerModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropIndex("dbo.GMapsMarkerModels", new[] { "BlogData_BlogDataId" });
            DropTable("dbo.GMapsMarkerModels");
        }
    }
}
