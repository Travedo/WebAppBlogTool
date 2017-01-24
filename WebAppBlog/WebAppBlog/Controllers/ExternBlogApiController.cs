using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppBlog.Controllers
{
    [RoutePrefix("api/ExternBlog")]
    public class ExternBlogApiController : ApiController
    {

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
