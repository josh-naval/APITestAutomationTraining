using Session2_1.Model;
using Session2_1.Utilities;
using System.Net;

namespace Session2_1.Tests
{
    [TestClass]
    public class Homework
    {
        private readonly static string baseUrl = "https://petstore.swagger.io/v2";

        private static HttpClientHelper clientHelper;

        private Pet _myPet;

        [TestInitialize]
        public void Init()
        {
            clientHelper = new(baseUrl);
            clientHelper.AddRequestHeaders("Accept", "application/json");

            string[] photoUrls = { "https://www.animalspot.net/great-potoo.html" };

            _myPet = new Pet()
            {
                Name = "Ghost",
                Status = "Available",
                PhotoUrls = photoUrls,
                Category = new Category() { Name = "Bird" },
                Tags = new Tag[] { new Tag { Name = "Bird" } }
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (_myPet != null)
                clientHelper.DeleteRequest($"/pet/{_myPet.Id}");
        }

        [TestMethod]
        public void UpdatePet()
        {
            var newName = "Ghost Bird";

            CreatePet();

            _myPet.Name = newName;

            var updatedPet = clientHelper.PutRequest("/pet", _myPet);

            Assert.AreEqual(clientHelper.Response.StatusCode, HttpStatusCode.OK, "Verify Status Code is OK");
            Assert.AreEqual(updatedPet.Name, _myPet.Name, "Verify Pet Details Are Updated");
        }

        private void CreatePet()
        {
            _myPet = clientHelper.PostRequest("/pet", _myPet);
            Assert.AreEqual(clientHelper.Response.StatusCode, HttpStatusCode.OK, "Verify Status Code is OK");
        }


    }
}