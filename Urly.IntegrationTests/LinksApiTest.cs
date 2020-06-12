using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urly.Dto;
using Urly.WebApi;

namespace Urly.IntegrationTests
{
    [TestClass]
    public class LinksApiTest
    {
        [DataTestMethod]
        [DataRow("b", "https://urly.dev/abc")]
        [DataRow("c", "http://urly.dev/123")]
        [DataRow("d", "urly/xyz")]
        public async Task GetLinkIsCorrect(string code, string expected)
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            HttpResponseMessage response = await client.GetAsync($"/api/v1/links/{code}");
            response.EnsureSuccessStatusCode();

            var linkDto = await JsonHelper.Deserialize<LinkDto>(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expected, linkDto.FullUrl);
            Assert.AreEqual(code, linkDto.ShortCode);
        }

        [TestMethod]
        public async Task PostLinkIsCorrect()
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            var createLinkDto = new CreateLinkDto { FullUrl = "link/qwe?a=1&b=2" };
            string json = JsonHelper.Serialize(createLinkDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"/api/v1/links", data);
            response.EnsureSuccessStatusCode();

            var linkDto = await JsonHelper.Deserialize<LinkDto>(response.Content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("link/qwe?a=1&b=2", linkDto.FullUrl);
            Assert.AreEqual("e", linkDto.ShortCode);
        }

        [TestMethod]
        public async Task PostAndGetLinkIsCorrect()
        {
            var factory = new UrlyWebApplicationFactory<Startup>();
            HttpClient client = factory.CreateClient();

            var createLinkDto = new CreateLinkDto { FullUrl = "link/qwe?a=1&b=2" };
            string json = JsonHelper.Serialize(createLinkDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync($"/api/v1/links", data);

            HttpResponseMessage response = await client.GetAsync($"/api/v1/links/e");
            var linkDto = await JsonHelper.Deserialize<LinkDto>(response.Content);

            Assert.AreEqual("link/qwe?a=1&b=2", linkDto.FullUrl);
            Assert.AreEqual("e", linkDto.ShortCode);
        }
    }
}
