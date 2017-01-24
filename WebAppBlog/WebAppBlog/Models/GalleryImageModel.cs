using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class GalleryImageModel
    {
        public int GalleryImageModelId { get; set; }
        public virtual GalleryModel GalleryModel { get; set; }
        public string GalleryModelId { get; set; }
        public string Base64 { get; set; }
       
    }
}