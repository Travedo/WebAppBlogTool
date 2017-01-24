using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class TextModel
    {
        public int TextModelId { get; set; }
        public virtual BlogData BlogData { get; set; }
        public string BlogDataId { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
    }
}