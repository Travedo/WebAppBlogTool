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
        private List<ImageWrapper> blogimages;
        public List<ImageWrapper> BlogImages { get { return blogimages; }set { blogimages = value; } }

        public BlogDataService()
        {
            BlogImages = new List<ImageWrapper>();
        }

        public void AddImages(ImageWrapper images)
        {
            BlogImages.Add(images);
        }

        public Blog GetBlogData()
        {
            return blog;
        }

        public List<ImageWrapper> GetImages()
        {
            return BlogImages;
        }

        public void SetBlogData(Blog blog)
        {
            this.blog = blog;
        }

        
    }
}