using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class BlogData 
    {
        public int BlogDataId { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public virtual ICollection<ImageModel> ImageModels { get; set; }


    }

    
}