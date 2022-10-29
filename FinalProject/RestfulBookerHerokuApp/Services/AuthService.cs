using FinalProject.RestfulBookerHerokuApp.Resources;
using FinalProject.RestfulBookerHerokuApp.TestData;
using FinalProject.WebServiceClients;

namespace FinalProject.HttpClient.Services
{
    public class AuthService
    {
        protected IClient client;

        public AuthService(IClient client)
        {
            this.client = client;
        }

        protected string GetAuthToken()
        {
            return client.PostRequest(Endpoints.Auth, UserGenerator.GetUser()).Token;
        }
    }
}
