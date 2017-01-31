using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppBlog.Models;
using WebAppBlog.Models.Blog;
using WebAppBlog.Services;

namespace WebAppBlog.Controllers
{
    [RoutePrefix("api/ExternBlogApi")]
    public class ExternBlogApiController : ApiController
    {

        private IBlogDataService blogService;
        public ExternBlogApiController(IBlogDataService service)
        {
            blogService = service;
        }

        [HttpGet]
        [Route("CreateBlog")]
        public HttpResponseMessage CreateBlog()
        {
            if (blogService.GetBlog() != null)
            {

                ApplicationDbContext context = new ApplicationDbContext();

                var user = context.Users.Find(User.Identity.GetUserId());

                var data = blogService.GetBlog();
                List<TextModel> texts = new List<TextModel>();
                List<ImageModel> images = new List<ImageModel>();
                List<VideoModel> videos = new List<VideoModel>();
                List<GalleryModel> gallerys = new List<GalleryModel>();
                List<GMapsMarkerModel> gmapsmarker = new List<GMapsMarkerModel>();
                foreach (var text in data.Elements)
                {
                    if (text is TextElement)
                    {
                        texts.Add(new TextModel { Text = text.value, Position = text.position });
                    }
                    else if (text is ImageElement)
                    {
                        var image = text as ImageElement;

                        images.Add(new ImageModel { Base64 = image.base64, Position = image.position });
                    }
                    else if (text is GalleryElement)
                    {

                        var gallery = text as GalleryElement;
                        var galleryImage = new List<GalleryImageModel>();
                        gallery.Images.ForEach(image => { galleryImage.Add(new GalleryImageModel { Base64 = image.base64 }); });
                        gallerys.Add(new GalleryModel { Position = gallery.position, ClassName = gallery.ClassName, GalleryImageModels = galleryImage });

                    }
                    else
                    {
                        var video = text as VideoElement;
                        videos.Add(new VideoModel { Source = video.Src, Position = video.position });
                    }
                    
                }

                foreach(var marker in data.GMapsMarker)
                {
                    gmapsmarker.Add(new GMapsMarkerModel { Latitude=marker.Latitude, Longitude=marker.Longitude });
                }

               

                //add everything to db
                string externaluserid = String.Format("{0}:{1}:{2}", user.Id, user.LastName, Guid.NewGuid().ToString());
                BlogData blog = new BlogData { ApplicationUser = user, Title = data.Title, Subtitle = data.Subtitle, GalleryModels = gallerys, ImageModels = images, TextModels = texts, GMapsMarkerModels=gmapsmarker, ExternalId = Guid.NewGuid(), ExternalUser = externaluserid, IsVisibleFromOutside = false, VideoModels = videos };
                context.BlogDatas.Add(blog);

                context.SaveChanges();
                context.Dispose();

                //clear service
                blogService.clearData();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(blog.BlogDataId.ToString())
                };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Preview Data service is empty. Please make sure data is pushed to preview or use Post /CreateBlogWithoutPreview")
                };
            }
        }

        [HttpGet]
        [Route("DeleteBlog")]
        public HttpResponseMessage DeleteBlog(int id)
        {

            return null;
        }

        [HttpPost]
        [Route("DeleteText")]
        public HttpResponseMessage DeleteText(HttpRequestMessage request)
        {

            return null;
        }


        [HttpPost]
        [Route("DeleteImage")]
        public HttpResponseMessage DeleteImage(HttpRequestMessage request)
        {
            return null;
        }

        [HttpPost]
        [Route("DeleteGallery")]
        public HttpResponseMessage DeleteGallery(HttpRequestMessage request)
        {

            return null;
        }

        [HttpPost]
        [Route("AddGallery")]
        public HttpResponseMessage AddGallery(HttpRequestMessage request)
        {

            return null;
        }

        [HttpPost]
        [Route("AddImage")]
        public HttpResponseMessage AddImage(HttpRequestMessage request)
        {

            return null;
        }

        [HttpPost]
        [Route("AddText")]
        public HttpResponseMessage AddText(HttpRequestMessage request)
        {

            return null;
        }
    }
}
