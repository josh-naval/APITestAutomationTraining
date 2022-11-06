using CountryInfoServiceReference;
using System.Diagnostics.Metrics;

namespace FinalProject.SoapClient
{
    [TestClass]
    public class SoapClientTest
    {
        private static CountryInfoServiceSoapTypeClient client = new(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        private List<tCountryCodeAndName> GetCountryCodesAndNames() => client.ListOfCountryNamesByCode();

        private string GetRandomCountryCode(List<tCountryCodeAndName> countryCodeAndNames)
        {
            var random = new Random();
            var number = random.Next(countryCodeAndNames.Count);
            var country = countryCodeAndNames.ElementAt(number);
            return $"{country.sISOCode}:{country.sName}";
        }

        private List<string> GetFiveRandomCounties()
        {
            var countries = new List<string>();
            for (int counter = 0; counter < 5; counter++)
            {
                countries.Add(GetRandomCountryCode(GetCountryCodesAndNames()));
            }

            return countries;
        }

        [TestMethod]
        public void VerifyFullCountryInfoSoapEndpoint()
        {
            var countryCode = GetRandomCountryCode(GetCountryCodesAndNames());
            var country = client.FullCountryInfo(countryCode);

            var countryCodeAndName = countryCode.Split(":");

            Assert.AreEqual(countryCodeAndName[0], country.sISOCode);
            Assert.AreEqual(countryCodeAndName[1], country.sName);
        }

        [TestMethod]
        public void VerifyCountryISOCodeSoapEndpoint()
        {
            var fiveRandomCountries = GetFiveRandomCounties();

            fiveRandomCountries.ForEach(country =>
            {
                var countryDetails = country.Split(":");
                var countryIsoCode = client.CountryISOCode(countryDetails[1]);
                Assert.AreEqual(countryDetails[0], countryIsoCode);
            });

        }
    }
}