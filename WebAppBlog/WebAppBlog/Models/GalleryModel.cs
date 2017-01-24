using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class GalleryModel
    {
        public int GalleryModelId { get; set; }
        public virtual BlogData BlogData { get; set; }
        public string BlogDataId { get; set; }
        public virtual ICollection<GalleryImageModel> GalleryImageModels { get; set; }
        public string ClassName { get; set; }
        public int Position { get; set; }
    }
}