using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(string userName) :base(userName)
        {

        }
        public DateTime DateOfBirth { get; set; }
    }

    public class AppIdentityDbContext:IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("IdentityTestDb") { }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}