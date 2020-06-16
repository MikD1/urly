using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urly.WebApi;

namespace Urly.IntegrationTests
{
    [TestClass]
    public class RedirectTest
    {
        [DataTestMethod]
        [DataRow("b", "https://urly.dev/abc")]
        [DataRow("c", "http://urly.dev/123")]
        [DataRow("d", "urly/xyz")]
        public async Task RedirectIsCorrect(string code, string expected)
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            var options = new WebApplicationFactoryClientOptions { AllowAutoRedirect = false };
            HttpClient client = factory.CreateClient(options);

            HttpResponseMessage response = await client.GetAsync($"/{code}");

            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            Assert.AreEqual(expected, response.Headers.Location.ToString());
        }

        [TestMethod]
        public async Task GetNotFoundIfCodeNotExist()
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("/abc");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
