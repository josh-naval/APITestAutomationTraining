using FinalProject.RestfulBookerHerokuApp.Models;
using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.WebServiceClients.RestSharp;

namespace FinalProject
{
    [TestClass]
    public class BaseTest
    {
        protected RestSharpClientHelper? restSharpClientHelper;

        [TestInitialize]
        public void BaseClassInit()
        {
            restSharpClientHelper = InitializeRestSharpClient();
        }

        protected RestSharpClientHelper InitializeRestSharpClient()
        {
            var client = new RestSharpClientHelper(Endpoints.BASE_URL);
           
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

            return restSharpClientHelper!.PostRequest(Endpoints.Auth, user).Result.Token;
        }
    }
}