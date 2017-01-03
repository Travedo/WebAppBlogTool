using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class Images
    {
        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public string base64 { get; set; }
    }
}