using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Models;
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
        private ApplicationUserManager userManager;
        public BlogController(IBlogDataService service, ApplicationUserManager userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public ActionResult Overview()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var user = context.Users.Find(User.Identity.GetUserId());
            BlogOverviewViewModel blogvm = new BlogOverviewViewModel();
            blogvm.usersBlogs = new List<BlogOutput>();

             //find by user id
             if(user!=null) { 
             var lists = context.BlogDatas.Where(blog => blog.ApplicationUserId==user.Id).ToList();

                
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
    Request.ApplicationPath.TrimEnd('/') + "/";

                foreach (var blog in lists)
            {
                    string externalUrl = String.Format("{0}ExternBlog/ViewBlogFromExtern/?userid={1}&blogid={2}",baseUrl,blog.ExternalUser, blog.ExternalId);
                    blogvm.usersBlogs.Add(new BlogOutput { Title=blog.Title, Subtitle=blog.Subtitle, id=blog.BlogDataId, IsAccessible=blog.IsVisibleFromOutside,ExternalUrl=externalUrl });
            }

            }
            return View(blogvm);
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
            if (blog != null && !String.IsNullOrEmpty(blog.titel)) {
            data.Title = blog.titel;
            data.Subtitle = blog.subtitel;
            data.Elements = new List<Element>();
            data.GMapsMarker = new List<GMapsMarker>();

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

            
            data.GMapsMarker.AddRange(blog.gmapsMarker);
                if (data.GMapsMarker.Count > 0) data.IsGoogleMapsVisible = true;

            service.SetBlog(data);
            //shove elements into view or display
            return View(data);
            }
            else
            {
                data.ErrorMessage = "No preview data available. Please add elements to your blog and then hit preview.";
                return View(data);
            }
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
