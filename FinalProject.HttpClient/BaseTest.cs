using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.WebServiceClients.NetHttpClient;

namespace FinalProject
{
    [TestClass]
    public class BaseTest
    {
        protected HttpClientHelper InitializeHttpClient()
        {
            var client = new HttpClientHelper(Endpoints.BASE_URL);
            client.AddRequestHeaders("Accept", "application/json");

            return client;
        }
    }
}