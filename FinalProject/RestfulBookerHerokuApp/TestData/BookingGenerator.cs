using FinalProject.RestfulBookerHerokuApp.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.TestData
{
    public static class BookingGenerator
    {
        private static string dateFormat = "yyyy-MM-dd";

        public static Booking GetBookingData() 
        {
            return new Booking()
            {
                BookingDetails = new BookingDetails()
                {
                    FirstName = "Rhaenyra",
                    LastName = "Targaryen",
                    TotalPrice = 8000,
                    DepositPaid = true,
                    BookingDates = new BookingDates()
                    {
                        CheckIn = DateTime.Today.ToString(dateFormat),
                        CheckOut = DateTime.Today.AddDays(3).ToString(dateFormat)
                    },
                    AdditionalNeeds = "Buffet Package"
                }
            };
        }

        public static Booking UpdateBookingData(Booking booking)
        {
            // Update data
            booking.BookingDetails.FirstName = "Alicent";
            booking.BookingDetails.LastName = "Hightower";

            return booking;
        }
    }
}
