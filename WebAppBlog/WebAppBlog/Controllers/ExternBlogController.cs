using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBlog.Controllers
{
    public class ExternBlogController : Controller
    {
        // GET: ExternBlog
        public ActionResult Index()
        {
            return View();
        }
    }
}