using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Models;
using WebAppBlog.Models.Blog;

namespace WebAppBlog.Controllers
{
    public class ExternBlogController : Controller
    {


        private ApplicationUserManager userManager;
        public ExternBlogController() { }

        public ExternBlogController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        // GET: ExternBlog
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());

            //find by user id
            var lists = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id).ToList();


            var viewmodel = new ExternBlogViewModel { };


            return View(viewmodel);
        }

        public ActionResult Delete(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());


            if (user != null) { 
            
            //find by user id
            var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Single();

                context.BlogDatas.Remove(blogdata);
                context.SaveChanges();
            }
            return RedirectToAction("Overview", "Blog");
        }

        public ActionResult ShowBlog(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());
            //find by user id
            var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId==id).ToList().Single();

            List<Element> elements = GenerateElements(blogdata.GalleryModels,blogdata.ImageModels, blogdata.TextModels);
            elements = elements.OrderBy(x => x.position).ToList();
            var viewmodel = new ExternBlogViewModel { Title= blogdata.Title, Subtitle=blogdata.Subtitle, Elements=elements };


            return View(viewmodel);
        }

        private List<Element> GenerateElements(ICollection<GalleryModel> galleryModels, ICollection<ImageModel> imageModels, ICollection<TextModel> textModels)
        {
            List<Element> elements = new List<Element>();

            foreach (var text in textModels)
            {
                elements.Add(new TextElement { position=text.Position, value=text.Text });
            }

            foreach (var image in imageModels)
            {
                elements.Add(new ImageElement { position = image.Position, base64= image.Base64 });
            }

            foreach (var gallery in galleryModels)
            {
                List<Images> imgs = new List<Images>();

                gallery.Images.ForEach(img => imgs.Add(new Images {base64=img.Base64 }));


                elements.Add(new GalleryElement { position = gallery.Position, Images=imgs });
            }



            return elements;
        }
    }
}