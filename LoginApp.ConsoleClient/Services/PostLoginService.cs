using LoginApp.ConsoleClient.Dtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LoginApp.ConsoleClient.Services
{
    public class PostLoginService(HttpClient client, string baseUrl)
    {
        private const string apiPostLoginPath = "/api/post-login/logins";
        private readonly HttpClient _client = client;
        private readonly string _baseUrl = baseUrl;

        public async Task<bool> SendLoginInfoAsync(string token)
        {            
            var uri = new Uri(_baseUrl + apiPostLoginPath);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = PrepareClientInfoContent() };            
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _client.SendAsync(requestMessage);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }            
        }

        private static HttpContent PrepareClientInfoContent()
        {
            var clientInfo = new ClientInfo() { ClientType = "console" };
            var json = JsonSerializer.Serialize(clientInfo);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
