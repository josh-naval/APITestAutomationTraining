using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using System.Net;

namespace FinalProject.HttpClient
{
    [TestClass]
    public class HttpClientTest : BaseTest
    {
        private static BookingWrapper s_bookingWrapper;
        private static string dateFormat = "yyyy-MM-dd";

        [TestInitialize]
        public void TestInit()
        {
            if (s_bookingWrapper != null)
                return;

            s_bookingWrapper = new BookingWrapper()
            {
                Booking = new Booking()
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

        [TestMethod]
        public void A_CreateBooking()
        {
            Booking booking = s_bookingWrapper.Booking;
            var bookingWrapperResponse = _httpClientHelper.PostRequest(Endpoints.CreateBooking, s_bookingWrapper);
            var serverBookingDetails = _httpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(bookingWrapperResponse.BookingId.ToString()), null);
            s_bookingWrapper = bookingWrapperResponse;

            Assert.AreEqual(HttpStatusCode.OK, _httpClientHelper.Response.StatusCode);
            Assert.AreNotEqual(0, bookingWrapperResponse.BookingId);
            AssertBookingDetails(booking, serverBookingDetails);
        }

        [TestMethod]
        public void B_UpdateBooking()
        {
            Booking booking = s_bookingWrapper.Booking;

            // Update data
            booking.FirstName = "Alicent";
            booking.LastName = "Hightower";

            var bookingId = s_bookingWrapper.BookingId.ToString();

            _httpClientHelper.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            _httpClientHelper.PutRequest(Endpoints.UpdateBooking(bookingId), booking);
            var response = _httpClientHelper.Response;

            // Get Updated
            var serverBookingDetails = _httpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(bookingId), null);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, bookingId);
            AssertBookingDetails(booking, serverBookingDetails);

        }

        [TestMethod]
        public void C_RemoveCreatedBookings()
        {
            _httpClientHelper.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            _httpClientHelper.DeleteRequest(Endpoints.DeleteBooking(s_bookingWrapper.BookingId.ToString()));

            Assert.AreEqual(HttpStatusCode.Created, _httpClientHelper.Response.StatusCode);
        }

        [TestMethod]
        public void D_GetRemovedCreatedBookings()
        {
            _httpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(s_bookingWrapper.BookingId.ToString()), null);

            Assert.AreEqual(HttpStatusCode.NotFound, _httpClientHelper.Response.StatusCode);
        }

        private void AssertBookingDetails(Booking expectedBooking, Booking actualBooking)
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
