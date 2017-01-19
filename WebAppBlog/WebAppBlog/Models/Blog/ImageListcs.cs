using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class ImageListcs
    {
        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public Boolean gallery { get; set; }

        [JsonProperty]
        public string galleryName { get; set; }

        [JsonProperty]
        public int position { get; set; }
    }
}