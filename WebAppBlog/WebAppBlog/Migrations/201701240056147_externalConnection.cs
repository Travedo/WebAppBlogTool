namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class externalConnection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogDatas", "ExternalId", c => c.Guid(nullable: false));
            AddColumn("dbo.BlogDatas", "ExternalUser", c => c.String());
            AddColumn("dbo.BlogDatas", "IsVisibleFromOutside", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogDatas", "IsVisibleFromOutside");
            DropColumn("dbo.BlogDatas", "ExternalUser");
            DropColumn("dbo.BlogDatas", "ExternalId");
        }
    }
}
