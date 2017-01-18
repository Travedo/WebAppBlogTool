using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class Video
    {
        [JsonProperty]
        public string src { get; set; }

        [JsonProperty]
        public int position { get; set; }
    }
}