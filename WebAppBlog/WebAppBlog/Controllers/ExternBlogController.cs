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
    [Authorize]
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
            
                if(context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Any()) { 
                        var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Single();
                if (blogdata != null) { 
                     context.BlogDatas.Remove(blogdata);
                     context.SaveChanges();
                }
                }
            }
            return RedirectToAction("Overview", "Blog");
        }

        public ActionResult ShowBlog(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());
         
            if (user != null) { 
            var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId==id).ToList().Single();

            List<Element> elements = GenerateElements(blogdata.GalleryModels,blogdata.ImageModels, blogdata.TextModels);
            elements = elements.OrderBy(x => x.position).ToList();
            var viewmodel = new ExternBlogViewModel { Title= blogdata.Title, Subtitle=blogdata.Subtitle, Elements=elements };


            return View(viewmodel);
            }
            else
                return RedirectToAction("Overview", "Blog");
        }

        private List<Element> GenerateElements(ICollection<GalleryModel> galleryModels, ICollection<ImageModel> imageModels, ICollection<TextModel> textModels)
        {
            List<Element> elements = new List<Element>();

            textModels.ToList().ForEach(text => elements.Add(new TextElement { position = text.Position, value = text.Text }));

            imageModels.ToList().ForEach(image=> elements.Add(new ImageElement { position = image.Position, base64 = image.Base64 }));

            galleryModels.ToList().ForEach(gallery=> 
            {
                List<Images> imgs = new List<Images>();
                gallery.Images.ForEach(img => imgs.Add(new Images { base64 = img.Base64 }));
                elements.Add(new GalleryElement { position = gallery.Position, Images = imgs });
            });

            return elements;
        }
    }
}