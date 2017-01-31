using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppBlog.Models.Blog;
using WebAppBlog.Services;

namespace WebAppBlog.Controllers
{
    [RoutePrefix("api/BlogApi")]
    public class BlogApiController : ApiController
    {
        private IBlogDataService blogService;
        public BlogApiController(IBlogDataService service)
        {
            blogService = service;
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("CreateBlog")]
        public HttpResponseMessage CreateBlog(HttpRequestMessage request)
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
                var blogdata = JsonConvert.DeserializeObject<BlogWrapper>(data);
                if (blogService.GetGMapsMarker().Count > 0)
                    blogdata.blog.gmapsMarker = blogService.GetGMapsMarker();
                blogService.SetBlogData(blogdata.blog);
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
                blogService.AddImages(blogdata.images);
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
                blogService.AddGallery(blogdata.images);
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
                blogService.AddGMapsMarker(blogdata);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

        }

        [HttpGet]
        [Route("GetGMapsMarkers")]
        public List<GMapsMarker> GetGMapsMarkers()
        {

            return blogService.GetGMapsMarker();
        }

        [HttpPost]
        [Route("DeleteImage")]
        public HttpResponseMessage DeleteImage(HttpRequestMessage request)
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
                var blogdata = JsonConvert.DeserializeObject<Helper>(data);
              //  blogService.AddGMapsMarker(blogdata);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        [Route("DeleteGallery")]
        public HttpResponseMessage DeleteGallery(HttpRequestMessage request)
        {

            return null;
        }


        /* [HttpPost]
         [Route("CreateBlog")]
         public string CreateBlog(string value)
         {
             Debug.WriteLine(value);
             return "test";
         }*/

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}