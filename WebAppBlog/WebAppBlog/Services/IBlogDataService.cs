using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Services
{
    public interface IBlogDataService
    {
        void SetBlogData(Blog blog);
        Blog GetBlogData();

    }
}
