using FinalProject.RestfulBookerHerokuApp.CustomConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.Models
{
    [JsonConverter(typeof(BookingConverter))]
    public class BookingWrapper
    {
        [JsonProperty("bookingid")]
        public long BookingId { get; set; }

        [JsonProperty("booking")]
        public Booking Booking { get; set; }
    }
}
