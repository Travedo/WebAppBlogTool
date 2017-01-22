namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryModels",
                c => new
                    {
                        GalleryModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        ClassName = c.String(),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.GalleryModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId)
                .Index(t => t.BlogData_BlogDataId);
            
            CreateTable(
                "dbo.TextModels",
                c => new
                    {
                        TextModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Text = c.String(),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.TextModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId)
                .Index(t => t.BlogData_BlogDataId);
            
            AddColumn("dbo.ImageModels", "GalleryModel_GalleryModelId", c => c.Int());
            CreateIndex("dbo.ImageModels", "GalleryModel_GalleryModelId");
            AddForeignKey("dbo.ImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels", "GalleryModelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.ImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels");
            DropForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropIndex("dbo.TextModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.ImageModels", new[] { "GalleryModel_GalleryModelId" });
            DropIndex("dbo.GalleryModels", new[] { "BlogData_BlogDataId" });
            DropColumn("dbo.ImageModels", "GalleryModel_GalleryModelId");
            DropTable("dbo.TextModels");
            DropTable("dbo.GalleryModels");
        }
    }
}
