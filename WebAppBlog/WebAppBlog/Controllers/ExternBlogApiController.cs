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
        [Route("Delete")]
        public HttpResponseMessage Delete(int id)
        {

            return null;
        }
    }
}
