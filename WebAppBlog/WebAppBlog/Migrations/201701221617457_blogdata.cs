namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogDatas",
                c => new
                    {
                        BlogDataId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subtitle = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlogDataId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "ApplicationUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogDatas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BlogDatas", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "ApplicationUserId");
            DropTable("dbo.BlogDatas");
        }
    }
}
