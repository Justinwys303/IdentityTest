using IdentityTest.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityTest.Controllers
{
    public class RoleManagerController : Controller
    {
        private AppRoleManager _roleManager;
        private AppUserManager _userManager;
        public AppRoleManager RoleManager
        {
            get
            {
                return _roleManager = HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public AppUserManager UserManager
        {
            get
            {
                return _userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: RoleManager
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }
    }
}