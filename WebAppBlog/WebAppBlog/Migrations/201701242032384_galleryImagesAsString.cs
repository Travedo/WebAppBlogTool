namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class galleryImagesAsString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels");
            DropIndex("dbo.ImageModels", new[] { "GalleryModel_GalleryModelId" });
            DropColumn("dbo.ImageModels", "GalleryModel_GalleryModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageModels", "GalleryModel_GalleryModelId", c => c.Int());
            CreateIndex("dbo.ImageModels", "GalleryModel_GalleryModelId");
            AddForeignKey("dbo.ImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels", "GalleryModelId");
        }
    }
}
