using Session2_1.Model;

namespace Session2_1.Tests.TestData
{
    public class PetDataGenerator
    {
        public static Pet CreatePet() {
            string[] photoUrls = { "https://www.animalspot.net/great-potoo.html" };

            var pet = new Pet()
            {
                Name = "Ghost",
                Status = "Available",
                PhotoUrls = photoUrls,
                Category = new Category() { Name = "Bird" },
                Tags = new Tag[] { new Tag { Name = "Bird" } }
            };

            return pet;
        }
    }
}
