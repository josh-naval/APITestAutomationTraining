using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.Models
{
    public class BookingDates
    {
        [JsonProperty("checkin")]
        public string CheckIn { get; set; }

        [JsonProperty("checkout")]
        public string CheckOut { get; set; }

    }
}
