using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class BlogOutput
    {
        private string title;
        public string Title { get { return title; } set { title = value; } }

        private string subtitle;
        public string Subtitle { get { return subtitle; } set { subtitle = value; } }

        private List<Element> elemets;
        public List<Element> Elements { get { return elemets; } set { elemets = value; } }


    }
}