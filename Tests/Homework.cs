using Session2_1.Model;
using Session2_1.Utilities;
using System.Net;

namespace Session2_1.Tests
{
    [TestClass]
    public class Homework
    {
        private readonly static string baseUrl = "https://petstore.swagger.io/v2";

        private static RestSharpClientHelper clientHelper;

        private Pet _myPet;

        [TestInitialize]
        public void Init()
        {
            clientHelper = new(baseUrl);
            clientHelper.AddRequestParameters("Accept", "application/json");
            clientHelper.AddRequestParameters("Content-Type", "application/json");

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
        public async Task CleanUp()
        {
            if (_myPet != null)
                await clientHelper.DeleteRequest($"/pet/{_myPet.Id}");
        }

        [TestMethod]
        public async Task VerifyCreatedPet()
        {
            await CreatePet();

            var createdPet = await clientHelper.GetRequest<Pet>($"/pet/{_myPet.Id}", null);

            Assert.AreEqual(clientHelper.Response.StatusCode, HttpStatusCode.OK, "Verify Status Code is OK");
            Assert.AreEqual(createdPet.Name, _myPet.Name, "Verify Pet Name Are Correct");
            Assert.AreEqual(createdPet.Status, _myPet.Status, "Verify Pet Status Are Correct");
            Assert.AreEqual(createdPet.Category.Name, _myPet.Category.Name, "Verify Pet Category Are Correct");
            Assert.IsNotNull(createdPet.PhotoUrls.Select(url => url == _myPet.PhotoUrls[0]), "Verify Pet Photo Urls Are Correct");
            Assert.IsNotNull(createdPet.Tags.Select(tag => tag.Name == _myPet.Tags[0].Name), "Verify Pet Tags Are Correct");

        }

        private async Task CreatePet()
        {
            _myPet = await clientHelper.PostRequest("/pet", _myPet);
            Assert.AreEqual(clientHelper.Response.StatusCode, HttpStatusCode.OK, "Verify Status Code is OK");
        }


    }
}