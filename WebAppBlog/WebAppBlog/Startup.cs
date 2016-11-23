using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppBlog.Startup))]
namespace WebAppBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
