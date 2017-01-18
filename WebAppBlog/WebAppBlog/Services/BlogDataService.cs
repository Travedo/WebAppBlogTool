using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Services
{
    public class BlogDataService : IBlogDataService
    {
        private Blog blog;
        private List<Images> blogimages;
        public List<Images> BlogImages { get { return blogimages; }set { blogimages = value; } }
        public List<List<Images>> GalleryImages = new List<List<Images>>();
        public BlogDataService()
        {
            BlogImages = new List<Images>();
        }

        public void AddImages(Images[] images)
        {
            BlogImages.AddRange(images);
        }

        public void AddGallery(Images[] images) {
            GalleryImages.Add(images.ToList());
        }

        public List<List<Images>> GetGallery() {
            return GalleryImages;
        }



        public Blog GetBlogData()
        {
            return blog;
        }

        public List<Images> GetImages()
        {
            return BlogImages;
        }

        public void SetBlogData(Blog blog)
        {
            this.blog = blog;
        }

       
    }
}