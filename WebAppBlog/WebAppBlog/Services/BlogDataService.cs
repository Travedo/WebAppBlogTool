using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Services
{
    public class BlogDataService : IBlogDataService
    {
        private string title;

        public string getTitle()
        {
            return title;
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }
    }
}