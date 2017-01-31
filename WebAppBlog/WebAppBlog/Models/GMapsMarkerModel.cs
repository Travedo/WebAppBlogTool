using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class GMapsMarkerModel
    {
        public int GMapsMarkerModelId { get; set; }
        public virtual BlogData BlogData { get; set; }
        public string BlogDataId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}