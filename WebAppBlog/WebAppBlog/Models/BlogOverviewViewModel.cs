using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Models
{
    public class BlogOverviewViewModel
    {
        public List<BlogOutput> usersBlogs { get; set; }
    }
}