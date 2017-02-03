using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Services
{
    public interface IBlogEditService
    {
        List<Element> Retrieve();

        void DeleteById(int id);

        void Initialize(List<Element> elements);

        void SetCurrentBlogId(int id);

        int GetCurrentBlogId();

        void AddImages(Images[] images);
        List<Images> GetImages();

        void AddGallery(Images[] images);
        List<List<Images>> GetGallery();

        void AddGMapsMarker(GMapsMarker marker);
        List<GMapsMarker> GetGMapsMarker();

        void Clear();



    }
}