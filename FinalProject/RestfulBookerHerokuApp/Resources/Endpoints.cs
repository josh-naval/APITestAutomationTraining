using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.Resources
{
    public static class Endpoints
    {
        public const string BASE_URL = "https://restful-booker.herokuapp.com";

        public static string Auth => "/auth";

        public static string CreateBooking => "/booking";

        public static string GetBooking(string id) => $"/booking/{id}";

        public static string UpdateBooking(string id) => $"/booking/{id}";

        public static string DeleteBooking(string id) => $"/booking/{id}";

    }
}
