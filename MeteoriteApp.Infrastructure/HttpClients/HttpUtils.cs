using System.Net.Http.Json;
using System.Text.Json;

namespace MeteoriteApp.Infrastructure.HttpClients
{
    public class HttpUtils
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _defaultJsonOptions;

        public HttpUtils(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _defaultJsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }   

        public async Task<T?> GetAsync<T>(string url,
            JsonSerializerOptions? options = null, 
            CancellationToken cancellationToken = default) where T : class
        {
            var client = _httpClientFactory.CreateClient();

            using var response = await client.GetAsync(
                url,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(
                options ?? _defaultJsonOptions,
                cancellationToken);
        }
    }
}
