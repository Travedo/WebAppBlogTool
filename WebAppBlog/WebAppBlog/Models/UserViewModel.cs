using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBlog.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate{ get;set; }
        public bool EmailConfirmation { get; set; }
    }
}