namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogDatas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BlogDatas", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.BlogDatas", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.BlogDatas", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BlogDatas", "UserId");
            AddForeignKey("dbo.BlogDatas", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogDatas", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BlogDatas", new[] { "UserId" });
            AlterColumn("dbo.BlogDatas", "UserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.BlogDatas", name: "UserId", newName: "ApplicationUser_Id");
            CreateIndex("dbo.BlogDatas", "ApplicationUser_Id");
            AddForeignKey("dbo.BlogDatas", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
