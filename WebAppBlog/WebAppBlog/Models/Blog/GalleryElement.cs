using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class GalleryElement : Element
    {
        private List<Images> img;
        public List<Images> Images { get { return img; } set { img = value; } }

        public string ClassName { get; set; }

    }
}