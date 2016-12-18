using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Models.Blog;
using WebAppBlog.Services;

namespace WebAppBlog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public BlogController()
        { }

        private IBlogDataService service;
        public BlogController(IBlogDataService service)
        {
            this.service = service;
        }

        // GET: Blog
        public ActionResult Index()
        {
            BlogOutput data = new BlogOutput();
            var blog = service.GetBlogData();
            data.Title = blog.titel;
            data.Subtitle = blog.subtitel;
            data.Elements = new List<Element>();

            foreach (var text in blog.text)
            {
                data.Elements.Add(new TextElement { value = text.value, position = text.position });
            }

            data.Elements = data.Elements.OrderBy(x => x.position).ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
