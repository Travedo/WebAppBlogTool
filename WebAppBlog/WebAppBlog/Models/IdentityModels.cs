using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            BlogDatas = new List<BlogData>();
        }

        public int ApplicationUserId { get; set;}

        public DateTime Birthdate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<BlogData> BlogDatas { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            string fullName = this.FirstName + " " + this.LastName;
            if (String.IsNullOrEmpty(fullName)) fullName = "Annonymus";
            userIdentity.AddClaim(new Claim("FullName", fullName));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<BlogData> BlogDatas { get; set; }
       

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entity = modelBuilder.Entity<BlogData>();
            entity.HasMany<GalleryModel>(c => c.GalleryModels)
                
                 .WithOptional(x => x.BlogData)
                .WillCascadeOnDelete(true);

            entity.HasMany<ImageModel>(c => c.ImageModels)
               .WithOptional(x => x.BlogData)
              .WillCascadeOnDelete(true);

            entity.HasMany<TextModel>(c => c.TextModels)
            .WithOptional(x => x.BlogData)
           .WillCascadeOnDelete(true);

            entity.HasMany<VideoModel>(c => c.VideoModels)
                .WithOptional(x => x.BlogData)
                .WillCascadeOnDelete(true);

            entity.HasMany<GMapsMarkerModel>(c => c.GMapsMarkerModels)
               .WithOptional(x => x.BlogData)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<GalleryModel>()
                .HasMany(c => c.GalleryImageModels)
                .WithOptional(x => x.GalleryModel)
                .WillCascadeOnDelete(true);


        }
    }
}