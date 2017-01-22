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

        void AddImages(Images[] images);
        List<Images> GetImages();

        void AddGallery(Images[] images);
        List<List<Images>> GetGallery();

        void SetBlog(BlogOutput blog);
        BlogOutput GetBlog();

        void clearData();

    }
}
