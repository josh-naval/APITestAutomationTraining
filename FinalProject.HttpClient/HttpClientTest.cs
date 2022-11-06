using FinalProject.HttpClient.Services;
using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.TestData;
using System.Net;

namespace FinalProject.HttpClient
{
    [TestClass]
    public class HttpClientTest : BaseTest
    {
        private static Booking _booking;

        private BookingService bookingService;
        
        [TestInitialize]
        public void TestInit()
        {
            bookingService = new(InitializeHttpClient());
        }


        [TestMethod]
        public void A_CreateBooking()
        {
            _booking = bookingService.CreateBooking(BookingGenerator.GetBookingData());
            Assert.AreEqual(HttpStatusCode.OK, bookingService.GetResponse().StatusCode);

            var serverBookingDetails = bookingService.GetBooking(_booking.BookingId.ToString());
            Assert.AreEqual(HttpStatusCode.OK, bookingService.GetResponse().StatusCode);

            Assert.AreNotEqual(0, _booking.BookingId);
            AssertBookingDetails(_booking.BookingDetails, serverBookingDetails);
        }

        [TestMethod]
        public void B_UpdateBooking()
        {
            _booking = BookingGenerator.UpdateBookingData(_booking);
             var bookingId = _booking.BookingId.ToString();

            bookingService.UpdateBooking(_booking);
            Assert.AreEqual(HttpStatusCode.OK, bookingService.GetResponse().StatusCode);

            var serverBookingDetails = bookingService.GetBooking(bookingId);
            Assert.AreEqual(HttpStatusCode.OK, bookingService.GetResponse().StatusCode);

            Assert.AreNotEqual(0, bookingId);
            AssertBookingDetails(_booking.BookingDetails, serverBookingDetails);

        }

        [TestMethod]
        public void C_RemoveCreatedBookings()
        {
            bookingService.DeleteBooking(_booking.BookingId.ToString());
            Assert.AreEqual(HttpStatusCode.Created, bookingService.GetResponse().StatusCode);
        }

        [TestMethod]
        public void D_GetRemovedCreatedBookings()
        {
            var serverBookingDetails = bookingService.GetBooking(_booking.BookingId.ToString());
            Assert.AreEqual(HttpStatusCode.NotFound, bookingService.GetResponse().StatusCode);
        }

        private void AssertBookingDetails(BookingDetails expectedBooking, BookingDetails actualBooking)
        {
            Assert.AreEqual(expectedBooking.FirstName, actualBooking.FirstName);
            Assert.AreEqual(expectedBooking.LastName, actualBooking.LastName);
            Assert.AreEqual(expectedBooking.TotalPrice, actualBooking.TotalPrice);
            Assert.AreEqual(expectedBooking.DepositPaid, actualBooking.DepositPaid);
            Assert.AreEqual(expectedBooking.BookingDates.CheckIn, actualBooking.BookingDates.CheckIn);
            Assert.AreEqual(expectedBooking.BookingDates.CheckOut, actualBooking.BookingDates.CheckOut);
            Assert.AreEqual(expectedBooking.AdditionalNeeds, actualBooking.AdditionalNeeds);
        }
    }
}
