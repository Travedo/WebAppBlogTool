using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppBlog.Models;

namespace WebAppBlog.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;

        public UserController() {
        }

        public UserController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = new UserViewModel {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Birthdate=user.Birthdate,
                Email=user.Email
            };

            return View(model);
        }
    }
}