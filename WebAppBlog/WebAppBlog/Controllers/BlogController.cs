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

        public ActionResult Preview()
        {
            //TODO: move to service!
            BlogOutput data = new BlogOutput();
            var blog = service.GetBlogData();
            data.Title = blog.titel;
            data.Subtitle = blog.subtitel;
            data.Elements = new List<Element>();

            //add text to elements
            foreach (var text in blog.text)
            {
                data.Elements.Add(new TextElement { value = text.value, position = text.position });
            }

            //add image and slideshow data into elements
            data.Elements.AddRange( mapImages(service.GetImages(), blog.images.ToList(), service.GetGallery()));

            //add video into elements
            foreach (var video in blog.videos)
            {
                data.Elements.Add(new VideoElement { Src= video.src, position = video.position });
            }

            //sort, based on position
            data.Elements = data.Elements.OrderBy(x => x.position).ToList();

            //shove elements into view or display
            return View(data);
        }

        private IEnumerable<Element> mapImages(List<Images> list1, List<ImageListcs> list2, List<List<Images>> galleries)
        {
            List<Element> elements = new List<Element>();
            List<ImageListcs> tempGallery = new List<ImageListcs>();

            foreach (var img in list2)
            {
                if (img.gallery)
                {
                    tempGallery.Add(img);
                }
            }

            list2.RemoveAll(img => img.gallery);

            //map gallery data on service with gallery info send via blog creation
            GalleryElement g = new GalleryElement();
            g.Images = new List<Images>();
            foreach (var img in tempGallery)
            {
                foreach (var gallery in galleries)
                {
                   var match= gallery.Find(x => x.name.Equals(img.name));
                    if (match == null) continue;
                    g.Images.Add(new Images {base64=match.base64 });
                    if(g.position==0)
                         g.position = img.position;
                    g.ClassName = img.galleryName;
                }
            }
            if(g.Images.Count>0)
                elements.Add(g);

            //map images, if available
            foreach (var img in list2)
            {
                var imgElement = new ImageElement();
                list1.ForEach(x=> { if (x.name.Equals(img.name)) { imgElement.base64 = x.base64; imgElement.position = img.position; } });
                if(imgElement!=null) elements.Add(imgElement);
            }


            return elements;
        }

        public ActionResult Edit()
        {
            return View();
        }

    }

}
