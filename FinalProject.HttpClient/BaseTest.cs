using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.WebServiceClients.NetHttpClient;

namespace FinalProject
{
    [TestClass]
    public class BaseTest
    {
        protected HttpClientHelper _httpClientHelper;

        [TestInitialize]
        public void BaseClassInit()
        {
            _httpClientHelper = InitializeHttpClient();
        }

        protected HttpClientHelper InitializeHttpClient()
        {
            var client = new HttpClientHelper(Endpoints.BASE_URL);
            client.AddRequestHeaders("Accept", "application/json");

            return client;
        }

        protected string GetAuthToken() 
        {
            var user = new User()
            {
                Username = "admin",
                Password = "password123"
            };

            return _httpClientHelper.PostRequest(Endpoints.Auth, user).Token;
        }
    }
}