namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoModels",
                c => new
                    {
                        VideoModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Source = c.String(),
                        Position = c.Int(nullable: false),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.VideoModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId, cascadeDelete: true)
                .Index(t => t.BlogData_BlogDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropIndex("dbo.VideoModels", new[] { "BlogData_BlogDataId" });
            DropTable("dbo.VideoModels");
        }
    }
}
