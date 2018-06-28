using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityTest.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "City")]
        public Cities City { get; set; }

        public static List<SelectListItem> GetCitiesItems()
        {
            List<SelectListItem> cityItems = new List<SelectListItem>();
            cityItems.Add(new SelectListItem { Text = "shanghai", Value = "0" });
            cityItems.Add(new SelectListItem { Text = "Hangzhou", Value = "1" });
            cityItems.Add(new SelectListItem { Text = "NewYork", Value = "2" });
            cityItems.Add(new SelectListItem { Text = "Tokyo", Value = "3" });
            return cityItems;
        }

        //[Display(Name = "Country")]
        //public Countries Country { get; set; }
    }
}