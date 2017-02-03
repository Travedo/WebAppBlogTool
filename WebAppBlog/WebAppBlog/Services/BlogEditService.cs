using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Services
{
    public class BlogEditService : IBlogEditService
    {
        private List<Element> elements { get; set; }
        private int BlogId { get; set; }

        private List<Images> blogimages;
        public List<Images> BlogImages { get { return blogimages; } set { blogimages = value; } }
        public List<List<Images>> GalleryImages = new List<List<Images>>();

        private List<GMapsMarker> gmapsmarker;
        public List<GMapsMarker> GmapsMarker { get { return gmapsmarker; } set { gmapsmarker = value; } }

        public BlogEditService()
        {
            BlogImages = new List<Images>();
            GmapsMarker = new List<GMapsMarker>();
        }

        public void Initialize(List<Element> elements)
        {
            this.elements = elements;
        }

        public void DeleteById(int id)
        {
            elements.RemoveAt(id);
        }

        public List<Element> Retrieve()
        {
            return elements;
        }

        public void SetCurrentBlogId(int id)
        {
            BlogId = id;
        }

        public int GetCurrentBlogId()
        {
            return BlogId;
        }

        // add stuff that got dropped

        public void AddImages(Images[] images)
        {
            BlogImages.AddRange(images);
        }

        public void AddGallery(Images[] images)
        {
            GalleryImages.Add(images.ToList());
        }

        public List<List<Images>> GetGallery()
        {
            return GalleryImages;
        }

        public List<Images> GetImages()
        {
            return BlogImages;
        }

        public void AddGMapsMarker(GMapsMarker marker)
        {
            GmapsMarker.Add(marker);
        }

        public List<GMapsMarker> GetGMapsMarker()
        {
            return GmapsMarker;
        }

        //finally: delete all

        public void Clear()
        {
            GmapsMarker = new List<GMapsMarker>();
            elements = new List<Element>();
            BlogImages = new List<Images>();
            GalleryImages = new List<List<Images>>();
            BlogId = -1;
        }
    }
}