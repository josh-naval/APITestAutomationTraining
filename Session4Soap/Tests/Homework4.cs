using CountryInfoServiceReference;
namespace Session4Soap.Tests
{
    [TestClass]
    public class Homework4
    {
        private CountryInfoServiceSoapTypeClient? client;

        [TestInitialize]
        public void Init()
        {
            client = new(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void VerifyListOfCountryCodesSortingByCode()
        {
            var countries = client!.ListOfCountryNamesByCode();

            var countriesAsc = countries.OrderBy(countryName => countryName.sISOCode);

            Assert.IsTrue(countries.SequenceEqual(countriesAsc));
        }

        [TestMethod]
        public void VerifyInvalidCountryCodeIsNotFoundInTheDatabase()
        {
            var expectedMessage = "Country not found in the database";
            var countryName = client!.CountryName("sample");

            Assert.AreEqual(expectedMessage, countryName);
        }

        [TestMethod]
        public void VerifyCountryNameFunctionReturnsValidCountryName()
        {
            var countries = client!.ListOfCountryNamesByCode();
            var country = countries.Last();

            var countryName = client!.CountryName(country!.sISOCode);

            Assert.AreEqual(country.sName, countryName);
        }
    }
}