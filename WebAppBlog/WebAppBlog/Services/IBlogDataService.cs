using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppBlog.Services
{
    public interface IBlogDataService
    {
       void SetTitle(string title);
       string getTitle();

    }
}
