using FinalProject.RestfulBookerHerokuApp.CustomConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProject.RestfulBookerHerokuApp.Models
{
    [JsonConverter(typeof(BookingConverter))]
    public class Booking
    {
        [JsonProperty("bookingid")]
        public long BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingDetails BookingDetails { get; set; }
    }
}
