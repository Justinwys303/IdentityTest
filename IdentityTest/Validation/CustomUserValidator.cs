using IdentityTest.App_Start;
using IdentityTest.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityTest.Validation
{
    public class CustomUserValidator:UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr) : base(mgr) { }
        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            if(!user.Email.ToLower().EndsWith("@outlook.com"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("The address of Email only supports outlook");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}