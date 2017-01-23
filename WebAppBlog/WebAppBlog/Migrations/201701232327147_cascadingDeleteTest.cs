namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadingDeleteTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            AddForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId", cascadeDelete: true);
            AddForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId", cascadeDelete: true);
            AddForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            AddForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId");
            AddForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId");
            AddForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas", "BlogDataId");
        }
    }
}
