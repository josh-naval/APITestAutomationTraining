using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.WebServiceClients.RestSharp;

namespace FinalProject
{
    [TestClass]
    public class BaseTest
    {
        protected RestSharpClientHelper InitializeRestSharpClient()
        {
            var client = new RestSharpClientHelper(Endpoints.BASE_URL);
           
            client.AddRequestHeaders("Accept", "application/json");
            client.AddRequestHeaders("Content-Type", "application/json");

            return client;
        }
    }
}