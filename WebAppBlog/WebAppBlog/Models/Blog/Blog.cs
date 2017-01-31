using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class Blog
    {
        [JsonProperty]
        public string titel { get; set; }

        [JsonProperty]
        public string subtitel { get; set; }

        [JsonProperty]
        public IList<Text> text { get; set; }

        [JsonProperty]
        public IList<ImageListcs> images { get; set; }

        [JsonProperty]
        public IList<Video> videos { get; set; }

        [JsonProperty]
        public IList<GMapsMarker> gmapsMarker { get; set; }
    }
}