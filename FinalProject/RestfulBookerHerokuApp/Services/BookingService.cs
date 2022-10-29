using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.WebServiceClients;
using FinalProject.WebServiceClients.RestSharp;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FinalProject.HttpClient.Services
{
    public class BookingService : AuthService
    {
        public BookingService(IClient client) : base(client)
        {
            this.client = client;
        }

        public HttpResponseMessage GetResponse() => client.GetResponse();

        public RestResponse GetRestResponse()
        {
            var restSharpClientHelper = client as RestSharpClientHelper;
            return restSharpClientHelper!.Response;
        }

        public Booking CreateBooking(Booking booking)
        {
            return client.PostRequest(Endpoints.CreateBooking, booking);
        }

        public BookingDetails GetBooking(string bookingId)
        {
            return client.GetRequest<BookingDetails>(Endpoints.GetBooking(bookingId), null);
        }

        public BookingDetails UpdateBooking(Booking booking)
        {
            client.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            return client.PutRequest(Endpoints.UpdateBooking(booking.BookingId.ToString()), booking.BookingDetails);
        }

        public string DeleteBooking(string bookingId)
        {
            client.AddRequestHeaders("Cookie", $"token={GetAuthToken()}");
            return client.DeleteRequest(Endpoints.DeleteBooking(bookingId));
        }


    }
}
