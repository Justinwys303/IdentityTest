﻿using IdentityTest.App_Start;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityTest.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace IdentityTest.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager _userManager;
        public AccountController(){ }
        public AccountController(AppUserManager userManager)
        {
            _userManager = userManager;
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

        [AllowAnonymous]
        public ActionResult Register()
        {
            List<SelectListItem> cityItems = new List<SelectListItem>();
            cityItems.Add(new SelectListItem { Text = "shanghai", Value = "0" });
            cityItems.Add(new SelectListItem { Text = "Hangzhou", Value = "1" });
            cityItems.Add(new SelectListItem { Text = "NewYork", Value = "2" });
            cityItems.Add(new SelectListItem { Text = "Tokyo", Value = "3" });
            //ViewBag.Cities = new SelectList(cityItems);
            ViewData["Cities"] = cityItems;

            List<SelectListItem> countryItems = new List<SelectListItem>();
            countryItems.Add(new SelectListItem { Text = "China", Value = "0" });
            countryItems.Add(new SelectListItem { Text = "USA", Value = "1" });
            countryItems.Add(new SelectListItem { Text = "Japan", Value = "2" });
            countryItems.Add(new SelectListItem { Text = "None", Value = "3", Selected = true  });
            //ViewBag.Countries = new SelectList(countryItems);
            ViewData["Countries"] = countryItems;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, City = model.City };
                user.SetCountryFromCity(user.City);
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            List<SelectListItem> cityItems = new List<SelectListItem>();
            cityItems.Add(new SelectListItem { Text = "shanghai", Value = "0" });
            cityItems.Add(new SelectListItem { Text = "Hangzhou", Value = "1" });
            cityItems.Add(new SelectListItem { Text = "NewYork", Value = "2" });
            cityItems.Add(new SelectListItem { Text = "Tokyo", Value = "3" });
            //ViewBag.Cities = new SelectList(cityItems);
            ViewData["Cities"] = cityItems;

            List<SelectListItem> countryItems = new List<SelectListItem>();
            countryItems.Add(new SelectListItem { Text = "China", Value = "0" });
            countryItems.Add(new SelectListItem { Text = "USA", Value = "1" });
            countryItems.Add(new SelectListItem { Text = "Japan", Value = "2" });
            countryItems.Add(new SelectListItem { Text = "None", Value = "3", Selected = true });
            //ViewBag.Countries = new SelectList(countryItems);
            ViewData["Countries"] = countryItems;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    return View("Error", new string[] { "您已经登录！" });
            //}
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "无效的用户名和密码");
                }
                else
                {
                    var claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //claimsIdentity.AddClaims(LocationClaimsProvider.GetClaims(claimsIdentity));
                    //claimsIdentity.AddClaims(ClaimsRoles.CreateRolesFromClaims(claimsIdentity));
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIdentity);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

    }
}