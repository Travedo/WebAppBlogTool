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

        public Blog GetBlogData()
        {
            return blog;
        }

      

        public void SetBlogData(Blog blog)
        {
            this.blog = blog;
        }

        
    }
}