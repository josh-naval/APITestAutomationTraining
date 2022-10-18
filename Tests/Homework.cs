using Microsoft.VisualStudio.TestTools.UnitTesting;
using Session2_1.Model;
using Session2_1.Resources;
using Session2_1.Service;
using System.Net;

namespace Session2_1.Tests
{
    [TestClass]
    public class Homework: BaseTest
    { 
        private Pet _myPet;

        private PetService petService;

        [TestInitialize]
        public async Task Init()
        {
            petService = new(clientHelper);
            _myPet = await petService.CreatePet();
        }

        [TestCleanup]
        public async Task CleanUp()
        {
            if (_myPet != null)
                await clientHelper.DeleteRequest(Endpoints.DeletePetById(_myPet.Id));
        }

        [TestMethod]
        public async Task VerifyCreatedPet()
        {
            var createdPet = await clientHelper.GetRequest<Pet>(Endpoints.GetPetById(_myPet.Id), null);

            Assert.AreEqual(clientHelper.Response.StatusCode, HttpStatusCode.OK, "Verify Status Code is OK");
            Assert.AreEqual(createdPet.Name, _myPet.Name);
            Assert.AreEqual(createdPet.Status, _myPet.Status);
            Assert.AreEqual(createdPet.Category.Name, _myPet.Category.Name);
            Assert.IsTrue(Enumerable.SequenceEqual(createdPet.PhotoUrls, _myPet.PhotoUrls));
            Assert.IsNotNull(createdPet.Tags.Select(tag => _myPet.Tags.Select(tag2 => tag.Name == tag2.Name)));

        }
    }
}