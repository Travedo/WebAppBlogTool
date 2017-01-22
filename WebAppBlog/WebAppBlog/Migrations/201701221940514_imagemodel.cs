namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        ImageModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Base64 = c.String(),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId)
                .Index(t => t.BlogData_BlogDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropIndex("dbo.ImageModels", new[] { "BlogData_BlogDataId" });
            DropTable("dbo.ImageModels");
        }
    }
}
