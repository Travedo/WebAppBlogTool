using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class BlogOutput
    {
        public int id { get; set; }

        public string ErrorMessage { get; set; }

        public string ExternalUrl { get; set; }

        public bool IsAccessible { get; set; }

        public bool IsGoogleMapsVisible { get; set; }

        private string title;
        public string Title { get { return title; } set { title = value; } }

        private string subtitle;
        public string Subtitle { get { return subtitle; } set { subtitle = value; } }

        private List<Element> elemets;
        public List<Element> Elements { get { return elemets; } set { elemets = value; } }

        public List<GMapsMarker> GMapsMarker { get; set; }
    }
}