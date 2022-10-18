using Session2_1.Model;
using Session2_1.Resources;
using Session2_1.Tests.TestData;
using Session2_1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2_1.Service
{
    public class PetService
    {
        private RestSharpClientHelper clientHelper;

        public PetService(RestSharpClientHelper clientHelper)
        {
            this.clientHelper = clientHelper;
        }

        public async Task<Pet> CreatePet()
        {
            return await clientHelper.PostRequest(Endpoints.PostPet(), PetDataGenerator.CreatePet());
        }
    }
}
