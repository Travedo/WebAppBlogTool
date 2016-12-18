using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebAppBlog.Models;
using Microsoft.Owin.Security.DataProtection;

namespace WebAppBlog
{
    public class EmailService : IIdentityMessageService
    {
        private string fromMail = "";
        private string mailpw = "###";

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.

            SendEmail(message);

            return Task.FromResult(0);
        }

        private void SendEmail(IdentityMessage message)
        {
            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromMail, mailpw);

            try
            {
                // Create instance of message
                System.Net.Mail.MailMessage mymessage = new System.Net.Mail.MailMessage();

                // Add receiver
                mymessage.To.Add(message.Destination);

                // Set sender
                // In this case the same as the username
                mymessage.From = new System.Net.Mail.MailAddress(fromMail);

                // Set subject
                mymessage.Subject = message.Subject;

                // Set body of message
                mymessage.Body = message.Body;

                // Send the message
                client.Send(mymessage);

                // Clean up
                message = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not send e-mail. Exception caught: " + e);
            }
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.

            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            this.EmailService = new EmailService();
            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
            }

        }

        // Configure the application sign-in manager which is used in this application.
        public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
            {
                return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }
    }
}
