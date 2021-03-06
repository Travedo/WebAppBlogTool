﻿using System;
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

        private List<GMapsMarker> gmapsmarker;
        public List<GMapsMarker> GmapsMarker { get { return gmapsmarker; }set { gmapsmarker = value; } }

        BlogOutput blogdata { get; set; }

        public BlogDataService()
        {
            BlogImages = new List<Images>();
            GmapsMarker = new List<GMapsMarker>();
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

        public void SetBlog(BlogOutput blog)
        {
            blogdata = blog;
        }

        public BlogOutput GetBlog()
        {
            return blogdata;
        }

        public void clearData()
        {
            BlogImages = null;
            BlogImages = new List<Images>();
            GalleryImages = null;
            GalleryImages = new List<List<Images>>();
            GmapsMarker = new List<GMapsMarker>();
            blog = null;
            blogdata = null;
        }

        public void AddGMapsMarker(GMapsMarker marker)
        {
            GmapsMarker.Add(marker);
        }

        public List<GMapsMarker> GetGMapsMarker()
        {
            return GmapsMarker;
        }
    }
}