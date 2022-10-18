using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2_1.Resources
{
    public static class Endpoints
    {
        public const string BASE_URL = "https://petstore.swagger.io/v2";

        public static string GetPetById(long id) => $"/pet/{id}";

        public static string PostPet() => "/pet";

        public static string DeletePetById(long id) => $"/pet/{id}";
    }
}
