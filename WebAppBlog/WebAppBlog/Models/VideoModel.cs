using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class VideoModel
    {
        public int VideoModelId { get; set; }
        public virtual BlogData BlogData { get; set; }
        public string BlogDataId { get; set; }
        public string Source { get; set; }
        public int Position { get; set; }
    }
}