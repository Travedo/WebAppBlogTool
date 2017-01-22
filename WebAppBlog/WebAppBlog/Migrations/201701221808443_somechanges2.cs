namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogDatas", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogDatas", "ApplicationUserId");
        }
    }
}
