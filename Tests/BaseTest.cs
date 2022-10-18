using Session2_1.Resources;
using Session2_1.Utilities;

namespace Session2_1.Tests
{
    public class BaseTest
    {
        protected RestSharpClientHelper clientHelper;

        [TestInitialize]
        public void BaseInit()
        {
            clientHelper = new(Endpoints.BASE_URL);
            clientHelper.AddRequestHeaders("Accept", "application/json");
            clientHelper.AddRequestHeaders("Content-Type", "application/json");
        }
    }
}
