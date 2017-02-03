namespace WebAppBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogDatas",
                c => new
                    {
                        BlogDataId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Subtitle = c.String(),
                        ExternalId = c.Guid(nullable: false),
                        ExternalUser = c.String(),
                        IsVisibleFromOutside = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BlogDataId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUserId = c.Int(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GalleryModels",
                c => new
                    {
                        GalleryModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        ClassName = c.String(),
                        Position = c.Int(nullable: false),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.GalleryModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId, cascadeDelete: true)
                .Index(t => t.BlogData_BlogDataId);
            
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
                .ForeignKey("dbo.GalleryModels", t => t.GalleryModel_GalleryModelId, cascadeDelete: true)
                .Index(t => t.GalleryModel_GalleryModelId);
            
            CreateTable(
                "dbo.GMapsMarkerModels",
                c => new
                    {
                        GMapsMarkerModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.GMapsMarkerModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId, cascadeDelete: true)
                .Index(t => t.BlogData_BlogDataId);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        ImageModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Base64 = c.String(),
                        Position = c.Int(nullable: false),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId, cascadeDelete: true)
                .Index(t => t.BlogData_BlogDataId);
            
            CreateTable(
                "dbo.TextModels",
                c => new
                    {
                        TextModelId = c.Int(nullable: false, identity: true),
                        BlogDataId = c.String(),
                        Text = c.String(),
                        Position = c.Int(nullable: false),
                        BlogData_BlogDataId = c.Int(),
                    })
                .PrimaryKey(t => t.TextModelId)
                .ForeignKey("dbo.BlogDatas", t => t.BlogData_BlogDataId, cascadeDelete: true)
                .Index(t => t.BlogData_BlogDataId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.VideoModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.TextModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.ImageModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.GMapsMarkerModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.GalleryModels", "BlogData_BlogDataId", "dbo.BlogDatas");
            DropForeignKey("dbo.GalleryImageModels", "GalleryModel_GalleryModelId", "dbo.GalleryModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogDatas", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.VideoModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.TextModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.ImageModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.GMapsMarkerModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.GalleryImageModels", new[] { "GalleryModel_GalleryModelId" });
            DropIndex("dbo.GalleryModels", new[] { "BlogData_BlogDataId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BlogDatas", new[] { "ApplicationUserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VideoModels");
            DropTable("dbo.TextModels");
            DropTable("dbo.ImageModels");
            DropTable("dbo.GMapsMarkerModels");
            DropTable("dbo.GalleryImageModels");
            DropTable("dbo.GalleryModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlogDatas");
        }
    }
}
