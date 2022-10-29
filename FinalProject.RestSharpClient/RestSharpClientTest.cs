using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using System.Net;

namespace FinalProject.RestSharpClient
{
    [TestClass]
    public class RestSharpClientTest : BaseTest
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
        public async Task A_CreateBooking()
        {
            Booking booking = s_bookingWrapper.Booking;
            var bookingWrapperResponse = await restSharpClientHelper.PostRequest(Endpoints.CreateBooking, s_bookingWrapper);
            var serverBookingDetails = await restSharpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(bookingWrapperResponse.BookingId.ToString()), null);
            s_bookingWrapper = bookingWrapperResponse;

            Assert.AreEqual(HttpStatusCode.OK, restSharpClientHelper.Response.StatusCode);
            Assert.AreNotEqual(0, bookingWrapperResponse.BookingId);
            AssertBookingDetails(booking, serverBookingDetails);
        }

        [TestMethod]
        public async Task B_UpdateBooking()
        {
            Booking booking = s_bookingWrapper.Booking;

            // Update data
            booking.FirstName = "Alicent";
            booking.LastName = "Hightower";

            var bookingId = s_bookingWrapper.BookingId.ToString();

            restSharpClientHelper.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            await restSharpClientHelper.PutRequest(Endpoints.UpdateBooking(bookingId), booking);
            var response = restSharpClientHelper.Response;

            // Get Updated
            var serverBookingDetails = await restSharpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(bookingId), null);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, bookingId);
            AssertBookingDetails(booking, serverBookingDetails);

        }

        [TestMethod]
        public async Task C_RemoveCreatedBookings()
        {
            restSharpClientHelper.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            await restSharpClientHelper.DeleteRequest(Endpoints.DeleteBooking(s_bookingWrapper.BookingId.ToString()));

            Assert.AreEqual(HttpStatusCode.Created, restSharpClientHelper.Response.StatusCode);
        }

        [TestMethod]
        public async Task D_GetRemovedCreatedBookings()
        {
            await restSharpClientHelper.GetRequest<Booking>(Endpoints.GetBooking(s_bookingWrapper.BookingId.ToString()), null);

            Assert.AreEqual(HttpStatusCode.NotFound, restSharpClientHelper.Response.StatusCode);
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
