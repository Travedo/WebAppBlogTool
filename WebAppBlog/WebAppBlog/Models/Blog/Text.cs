using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class Text
    {
        [JsonProperty]
        public int position { get; set; }

        [JsonProperty]
        public string value { get; set; }
       
    }
}