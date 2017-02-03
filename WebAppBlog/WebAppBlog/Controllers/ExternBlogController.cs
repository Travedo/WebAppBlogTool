using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Models;
using WebAppBlog.Models.Blog;
using WebAppBlog.Services;

namespace WebAppBlog.Controllers
{
    [Authorize]
    public class ExternBlogController : Controller
    {
        IBlogEditService deletetempService;
        public ExternBlogController(IBlogEditService service)
        {
            deletetempService = service;
        }

        public ActionResult Delete(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());


            if (user != null) { 
            
                if(context.BlogDatas.Any(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id)) { 
                        var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Single();
                if (blogdata != null) { 
                     context.BlogDatas.Remove(blogdata);
                     context.SaveChanges();
                }
                }
            }
            return RedirectToAction("Overview", "Blog");
        }




        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                    ApplicationDbContext context = new ApplicationDbContext();
                    var user = context.Users.Find(User.Identity.GetUserId());

                    if (user != null && context.BlogDatas.Any(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id))
                    {

                        var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Single();

                        List<Element> elements = GenerateElements(blogdata.GalleryModels, blogdata.ImageModels, blogdata.TextModels, blogdata.VideoModels);

                    if (deletetempService.GetCurrentBlogId() == id && deletetempService.Retrieve() != null && deletetempService.Retrieve().Count >= 0)
                        elements = deletetempService.Retrieve();
                    else { 
                        elements = elements.OrderBy(x => x.position).ToList();
                        deletetempService.Clear();
                        deletetempService.Initialize(elements);
                        deletetempService.SetCurrentBlogId(id.GetValueOrDefault());
                    }


                   
                        var viewmodel = new ExternBlogViewModel { Title = blogdata.Title, Subtitle = blogdata.Subtitle, Elements = elements };


                        return View(viewmodel);
                    }
                }
            
            return RedirectToAction("Overview", "Blog");
        }

        public ActionResult ShowBlog(int? id)
        {
            if (id != null) { 
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());
         
            if (user != null && context.BlogDatas.Any(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id)) { 
                 
            var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId==id).ToList().Single();

            List<Element> elements = GenerateElements(blogdata.GalleryModels,blogdata.ImageModels, blogdata.TextModels, blogdata.VideoModels);
            elements = elements.OrderBy(x => x.position).ToList();

            bool gmapsVisibility = false;
            if (blogdata.GMapsMarkerModels.Count > 0)
                     gmapsVisibility = true;


            var viewmodel = new ExternBlogViewModel { Title= blogdata.Title, Subtitle=blogdata.Subtitle, Elements=elements,IsGoogleMapsVisible=gmapsVisibility };


            return View(viewmodel);
            }
            else
                return RedirectToAction("Overview", "Blog");
            }
            else
            {
                return RedirectToAction("Overview", "Blog");
            }
        }

        [AllowAnonymous]
        public ActionResult ViewBlogFromExtern(string userid, string blogid)
        {
            var userdata = userid.Split(':');
            Guid blogguid; 
            try { 
             blogguid = new Guid(blogid);
            }catch(FormatException e)
            {
                return null;
            }
            var uderid = userdata[0];
            ApplicationDbContext context = new ApplicationDbContext();
            if(context.BlogDatas.Any(blog => blog.ApplicationUserId == uderid && blog.ExternalId == blogguid)) {

                var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == uderid && blog.ExternalId == blogguid).ToList().Single();


                List<Element> elements = GenerateElements(blogdata.GalleryModels, blogdata.ImageModels, blogdata.TextModels, blogdata.VideoModels);
                elements = elements.OrderBy(x => x.position).ToList();

                bool gmapsVisibility = false;
                if (blogdata.GMapsMarkerModels.Count > 0)
                    gmapsVisibility = true;

                var viewmodel = new ExternBlogViewModel { Title = blogdata.Title, Subtitle = blogdata.Subtitle, Elements = elements, IsGoogleMapsVisible=gmapsVisibility };


                return View(viewmodel);
            }


            return null;
        }



        private List<Element> GenerateElements(ICollection<GalleryModel> galleryModels, ICollection<ImageModel> imageModels, ICollection<TextModel> textModels, ICollection<VideoModel> videoModels)
        {
            List<Element> elements = new List<Element>();

            textModels.ToList().ForEach(text => elements.Add(new TextElement { position = text.Position, value = text.Text }));

            imageModels.ToList().ForEach(image=> elements.Add(new ImageElement { position = image.Position, base64 = image.Base64 }));

            galleryModels.ToList().ForEach(gallery =>
            {
                List<Images> imgs = new List<Images>();
                gallery.GalleryImageModels.ToList().ForEach(img => imgs.Add(new Images { base64=img.Base64 }));
                elements.Add(new GalleryElement { position = gallery.Position, Images = imgs, ClassName=gallery.ClassName }); 
            });

            videoModels.ToList().ForEach(video => elements.Add( new VideoElement { Src=video.Source, position=video.Position } ));
            
            return elements;
        }

        
    }

}