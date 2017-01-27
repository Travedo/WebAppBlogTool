namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class galleryImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryImageModels",
                c => new
                    {
                        GalleryImageModelId = c.Int(nullable: false, identity: true),
                        GalleryModelId = c.String(),
                        Base64 = c.String(),
                        GalleryModel_GalleryModelId = c.Int(),
                    })
                .PrimaryKey(t => t.GalleryImageModelId)
                .ForeignKey("dbo.GalleryModels", t => t.GalleryModel_GalleryModelId)
                .Index(t => t.GalleryModel_GalleryModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels");
            DropIndex("dbo.GalleryImageModels", new[] { "GalleryModel_GalleryModelId" });
            DropTable("dbo.GalleryImageModels");
        }
    }
}
