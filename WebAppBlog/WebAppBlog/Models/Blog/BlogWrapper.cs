using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models.Blog
{
    public class BlogWrapper
    {
        [JsonProperty]
        public Blog blog { get; set; }
    }
}