using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urly.WebApi;

namespace Urly.IntegrationTests
{
    [TestClass]
    public class SwaggerTest
    {
        [TestMethod]
        public async Task SwaggerResponseIsCorrect()
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("/swagger");
            response.EnsureSuccessStatusCode();

            response = await client.GetAsync("/api-docs/swagger/v1.0/swagger.json");
            string responseString = await response.Content.ReadAsStringAsync();

            string infoTitle;
            using (JsonDocument document = JsonDocument.Parse(responseString))
            {
                infoTitle = document.RootElement.GetProperty("info").GetProperty("title").GetString();
            }

            Assert.AreEqual("Urly API", infoTitle);
        }
    }
}
