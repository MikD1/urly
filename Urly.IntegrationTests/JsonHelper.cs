using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Urly.IntegrationTests
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(data, options);
            return json;
        }

        public static T Deserialize<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            T data = JsonSerializer.Deserialize<T>(json, options);
            return data;
        }

        public static async Task<T> Deserialize<T>(HttpContent content)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = await content.ReadAsStringAsync();
            T data = JsonSerializer.Deserialize<T>(json, options);
            return data;
        }
    }
}
