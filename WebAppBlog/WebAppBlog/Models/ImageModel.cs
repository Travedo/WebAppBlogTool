using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class ImageModel
    {
        public int ImageModelId { get; set; }
        public virtual BlogData BlogData { get; set; }
        public string BlogDataId { get; set; }
        public string Base64 { get; set; }
        public int Position { get; set; }


    }
}