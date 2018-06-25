using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest.Models
{
    public class AppUser : IdentityUser
    {
        public Countries Country { get; set; }
        public Cities City { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.Shanghai:
                case Cities.Hangzhou:
                    Country = Countries.China;
                    break;
                case Cities.NewYork:
                    Country = Countries.USA;
                    break;
                case Cities.Tokyo:
                    Country = Countries.Japan;
                    break;
                default:
                    Country = Countries.None;
                    break;
            }
        }
    }

    public enum Countries
    {
        China,
        USA,
        Japan,
        None
    }
    public enum Cities
    {
        Shanghai,
        Hangzhou,
        NewYork,
        Tokyo
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