using FinalProject.RestfulBookerHerokuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.TestData
{
    public static class UserGenerator
    {
        public static User GetUser() 
        {
            return new User()
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
