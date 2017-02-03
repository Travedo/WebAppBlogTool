using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        IBlogEditService editService;
        public ExternBlogApiController(IBlogDataService service, IBlogEditService deleteteService)
        {
            blogService = service;
            editService = deleteteService;
        }

        [HttpGet]
        public HttpResponseMessage GetDataToBeRemovedFromBlog(int? id)
        {
            if (id != null)
            {
                if (editService.Retrieve() != null && editService.Retrieve().Count > 0)
                {
                    string idasString = editService.GetCurrentBlogId().ToString();
                    editService.DeleteById(id.GetValueOrDefault());

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {

                        Content = new StringContent(idasString)
                    };
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Please specify an id.")
                };
            };
            return null;
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
        [Route("GetGMapsMarkers")]
        public List<GMapsMarker> GetGMapsMarkers(int? id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.Find(User.Identity.GetUserId());

            if (user != null && id!=null && context.BlogDatas.Any(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id))
            {

                var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == user.Id && blog.BlogDataId == id).ToList().Single();
                List<GMapsMarker> gmapsmarker = new List<GMapsMarker>();
                foreach(var marker in blogdata.GMapsMarkerModels)
                {
                    gmapsmarker.Add(new GMapsMarker { Latitude=marker.Latitude, Longitude=marker.Longitude });
                }
                context.Dispose();

                return gmapsmarker;
            }else
            {
                return null;
            }
            
        }

        [HttpGet]
        [Route("GetGMapsMarkersForExtern")]
        public List<GMapsMarker> GetGMapsMarkersForExtern(string userid, string blogid)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Guid blogguid;
            try
            {
                blogguid = new Guid(blogid);
            }
            catch (FormatException e)
            {
                return null;
            }

            if (context.BlogDatas.Any(blog => blog.ApplicationUserId == userid && blog.ExternalId == blogguid))
            
                {
                var blogdata = context.BlogDatas.Where(blog => blog.ApplicationUserId == userid && blog.ExternalId == blogguid).ToList().Single();
                List<GMapsMarker> gmapsmarker = new List<GMapsMarker>();
                foreach (var marker in blogdata.GMapsMarkerModels)
                {
                    gmapsmarker.Add(new GMapsMarker { Latitude = marker.Latitude, Longitude = marker.Longitude });
                }
                context.Dispose();

                return gmapsmarker;
            }
            else
            {
                return null;
            }

        }


        [HttpPost] //put, currently not working on the test server
        [Route("UpdateBlog")]
        public HttpResponseMessage UpdateBlog(HttpRequestMessage request)
        {

            var stringdata = request.Content.ReadAsStringAsync().Result;
            if (stringdata == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Post data is empty")
                };
            }
            else
            {
                string externaluserid="";
                Guid externalid= new Guid();
                var blogdata = JsonConvert.DeserializeObject<BlogWrapper>(stringdata);
                if (editService.GetGMapsMarker().Count >= 0)
                    blogdata.blog.gmapsMarker = editService.GetGMapsMarker();

                var updateBlog = blogdata.blog;
                ApplicationDbContext context = new ApplicationDbContext();
                var user = context.Users.Find(User.Identity.GetUserId());
                int id = editService.GetCurrentBlogId();
                //delete old blog
                if (context.BlogDatas.Any(b => b.ApplicationUserId == user.Id && b.BlogDataId == id))
                {
                    var oldblog = context.BlogDatas.Where(b => b.ApplicationUserId == user.Id && b.BlogDataId == id).ToList().Single();
                    if (oldblog != null)
                    {
                        //keep external links
                        externaluserid = oldblog.ExternalUser;
                        externalid = oldblog.ExternalId;

                        //delete old blog
                        context.BlogDatas.Remove(oldblog);
                        context.SaveChanges();
                    }
                }

                List<TextModel> texts = new List<TextModel>();
                List<ImageModel> images = new List<ImageModel>();
                List<VideoModel> videos = new List<VideoModel>();
                List<GalleryModel> gallerys = new List<GalleryModel>();
                List<GMapsMarkerModel> gmapsmarker = new List<GMapsMarkerModel>();
                int textExisting = 0;
                var stillexistingelements = editService.Retrieve();

                //old data, that wasnt deleted, add first
                foreach (var img in stillexistingelements)
                {
                    if(img is TextElement)
                    {
                        var text = img as TextElement; 
                        texts.Add(new TextModel { Text = text.value, Position = text.position });
                        textExisting++;
                    }
                    else
                    if (img is ImageElement)
                    {
                        var image = img as ImageElement;

                        images.Add(new ImageModel { Base64 = image.base64, Position = image.position });
                    }
                    else if (img is GalleryElement)
                    {

                        var gallery = img as GalleryElement;
                        var galleryImage = new List<GalleryImageModel>();
                        gallery.Images.ForEach(image => { galleryImage.Add(new GalleryImageModel { Base64 = image.base64 }); });
                        gallerys.Add(new GalleryModel { Position = gallery.position, ClassName = gallery.ClassName, GalleryImageModels = galleryImage });

                    }
                    else if (img is VideoElement)
                    {
                        var video = img as VideoElement;

                        videos.Add(new VideoModel { Source = video.Src, Position = video.position });
                    }
                }

                //to match the array indexing start at 0
                textExisting--;

                while (textExisting >= 0)
                {
                    updateBlog.text.RemoveAt(textExisting);
                    textExisting--;
                }

                //new data added trough drag n drop
                int alreadyfiledPositions = stillexistingelements.Count;

                foreach (var text in updateBlog.text)
                {
                    texts.Add(new TextModel { Text = text.value, Position = text.position+ alreadyfiledPositions });
                }

                //add image and slideshow data into elements

                List<Element> imageElements = mapImages(editService.GetImages(), updateBlog.images.ToList(), editService.GetGallery());
               

                foreach(var img in imageElements)
                {
                    if (img is ImageElement)
                    {
                        var image = img as ImageElement;
                        if(image.base64!= null && image.position != 0) { 

                        images.Add(new ImageModel { Base64 = image.base64, Position = image.position+ alreadyfiledPositions });
                        }
                    }
                    else if (img is GalleryElement)
                    {

                        var gallery = img as GalleryElement;
                        var galleryImage = new List<GalleryImageModel>();
                        gallery.Images.ForEach(image => { galleryImage.Add(new GalleryImageModel { Base64 = image.base64 }); });
                        gallerys.Add(new GalleryModel { Position = gallery.position+ alreadyfiledPositions, ClassName = gallery.ClassName, GalleryImageModels = galleryImage });

                    }
                }

                

                //add video into elements
                foreach (var video in updateBlog.videos)
                {
                    videos.Add(new VideoModel { Source = video.src, Position = video.position+ alreadyfiledPositions });
                }


                foreach (var marker in updateBlog.gmapsMarker)
                {
                    gmapsmarker.Add(new GMapsMarkerModel { Latitude = marker.Latitude, Longitude = marker.Longitude });
                }

                //add everything to db
               
                BlogData blog = new BlogData { ApplicationUser = user, Title = blogdata.blog.titel, Subtitle = blogdata.blog.subtitel, GalleryModels = gallerys, ImageModels = images, TextModels = texts, GMapsMarkerModels = gmapsmarker, ExternalId = externalid, ExternalUser = externaluserid, IsVisibleFromOutside = false, VideoModels = videos };
                context.BlogDatas.Add(blog);

                context.SaveChanges();
                context.Dispose();
                editService.Clear();


                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        [Route("AddImages")]
        public HttpResponseMessage AddImages(HttpRequestMessage request)
        {
            var data = request.Content.ReadAsStringAsync().Result;
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Post data is empty")
                };
            }
            else
            {
                Debug.WriteLine(data);
                var blogdata = JsonConvert.DeserializeObject<ImageWrapper>(data);
                editService.AddImages(blogdata.images);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


        }


        [HttpPost]
        [Route("AddGallery")]
        public HttpResponseMessage AddGallery(HttpRequestMessage request)
        {
            var data = request.Content.ReadAsStringAsync().Result;
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Post data is empty")
                };
            }
            else
            {
                Debug.WriteLine(data);
                var blogdata = JsonConvert.DeserializeObject<ImageWrapper>(data);
                editService.AddGallery(blogdata.images);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


        }


        [HttpPost]
        [Route("AddGMapsMarker")]
        public HttpResponseMessage AddGMapsMarker(HttpRequestMessage request)
        {
            var data = request.Content.ReadAsStringAsync().Result;
            if (data == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Post data is empty")
                };
            }
            else
            {
                Debug.WriteLine(data);
                var blogdata = JsonConvert.DeserializeObject<GMapsMarker>(data);
                editService.AddGMapsMarker(blogdata);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

        }

        [HttpGet]
        [Route("CancelBlogUpdate")]
        public HttpResponseMessage CancelBlogUpdate()
        {
            editService.Clear();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        private List<Element> mapImages(List<Images> list1, List<ImageListcs> list2, List<List<Images>> galleries)
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
                    var match = gallery.Find(x => x.name.Equals(img.name));
                    if (match == null) continue;
                    g.Images.Add(new Images { base64 = match.base64 });
                    if (g.position == 0)
                        g.position = img.position;
                    g.ClassName = img.galleryName;
                }
            }
            if (g.Images.Count > 0)
                elements.Add(g);

            //map images, if available
            foreach (var img in list2)
            {
                var imgElement = new ImageElement();
                list1.ForEach(x => { if (x.name.Equals(img.name)) { imgElement.base64 = x.base64; imgElement.position = img.position; } });
                if (imgElement != null) elements.Add(imgElement);
            }


            return elements;
        }

    }
}
