using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Services;

namespace WebAppBlog.Controllers
{
    public class BlogController : Controller
    {
        private IBlogDataService service;
        public BlogController(IBlogDataService service)
        {
            this.service = service;

        }

        // GET: Blog
        public ActionResult Index()
        {
            return View(service);
        }
    }
}
