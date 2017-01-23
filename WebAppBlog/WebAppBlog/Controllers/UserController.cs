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

        //Post: User/ResendEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResendEmail()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.Id, "Account confirmation mail resend", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            return RedirectToAction("Index", "User");
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = new UserViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate.ToString("dd.mm.yyyy"),
                Email = user.Email,
                EmailConfirmation = getEmailConfirmation(user.EmailConfirmed),
                IsEmailConfirmed= user.EmailConfirmed

            };

            return View(model);
        }

        private string getEmailConfirmation(bool emailConfirmed)
        {
            if (emailConfirmed) return "Yes";
            else return "No";
        }
    }
}